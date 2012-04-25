using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace beanstalkapp_net
{
    public static class Beanstalk
    {
        internal static string ApiUrl = "https://{0}.beanstalkapp.com/api";

        internal static string Domain;
        internal static string Username;
        internal static string Password;

        public static void Initialize(string username, string password, string domain = null)
        {
            Username = username;
            Password = password;
            Domain = domain;
            ApiUrl = string.Format(ApiUrl, Domain);
        }

        internal static T Get<T>(string relativeUrl)
        {
            var jsonStr = GetJson(relativeUrl);

            var json = JObject.Parse(jsonStr);
            var firstChild = json.Values().FirstOrDefault();

            if (firstChild == null)
                return default(T);

            return firstChild.ToObject<T>();
        }

        public static IEnumerable<T> GetMany<T>(string relativeUrl)
        {
            var array = JArray.Parse(GetJson(relativeUrl));

            var arrayChildren = array.Select(a => a.Values().First());

            return arrayChildren.Select(f => f.ToObject<T>());
        }

        private static string GetJson(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
                throw new Exception("Relative URL must start with '/'.");

            using (var wc = SetupConnection())
                return wc.DownloadString(ApiUrl + relativeUrl);
        }

        public static void Update(string relativeUrl, string method)
        {
            Update(relativeUrl, method, null);
        }

        public static void Update(string relativeUrl, string method, object parameters)
        {
            if (!relativeUrl.StartsWith("/"))
                throw new Exception("Relative URL must start with '/'.");

            var serial = JsonConvert.SerializeObject(parameters);

            using (var wc = SetupConnection())
                wc.UploadString(ApiUrl + relativeUrl, method, serial);
        }

        private static WebClient SetupConnection()
        {
            var wc = new WebClient();

            wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            wc.Credentials = new NetworkCredential(Username, Password);

            return wc;
        }
    }
}
