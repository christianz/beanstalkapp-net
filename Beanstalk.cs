using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Initializes the Beanstalk API with your username, password, and subdomain.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="domain"></param>
        public static void Initialize(string username, string password, string domain = null)
        {
            Username = username;
            Password = password;
            Domain = domain;
            ApiUrl = string.Format(ApiUrl, Domain);
        }

        internal static T Get<T>(string relativeUrl)
        {
            var jsonStr = DownloadJsonString(relativeUrl);

            return Deserialize<T>(jsonStr);
        }

        internal static IEnumerable<T> GetMany<T>(string relativeUrl)
        {
            var array = JArray.Parse(DownloadJsonString(relativeUrl));

            var arrayChildren = array.Select(a => a.Values().First());

            return arrayChildren.Select(f => f.ToObject<T>());
        }

        internal static void Update(string relativeUrl, string method, object parameters = null)
        {
            UpdateInternal(relativeUrl, method, parameters);
        }

        internal static T Update<T>(string relativeUrl, string method, object parameters = null)
        {
            var result = UpdateInternal(relativeUrl, method, parameters);

            return Deserialize<T>(result);
        }

        private static T Deserialize<T>(string jsonStr)
        {
            var json = JObject.Parse(jsonStr);
            var firstChild = json.Values().FirstOrDefault();

            if (firstChild == null)
                return default(T);

            return firstChild.ToObject<T>();
        }

        private static string DownloadJsonString(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
                throw new Exception("Relative URL must start with '/'.");

            using (var wc = SetupConnection())
                return wc.DownloadString(ApiUrl + relativeUrl);
        }

        private static string UploadJsonString(string relativeUrl, string method, string data = "")
        {
            if (!relativeUrl.StartsWith("/"))
                throw new Exception("Relative URL must start with '/'.");

            using (var wc = SetupConnection())
            {
                try
                {
                    return wc.UploadString(ApiUrl + relativeUrl, method, data);
                }
                catch (WebException e)
                {
                    if (e.Status != WebExceptionStatus.ProtocolError)
                        throw;
                     
                    var response = ((HttpWebResponse) e.Response);
                    string responseStr;

                    using (var stream = response.GetResponseStream())
                    {
                        if (stream == null)
                            throw;

                        try
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                responseStr = reader.ReadToEnd();
                            }
                        }
                        catch
                        {
                            throw e;
                        }
                    }

                    return responseStr;
                }
            }
        }

        private static string UpdateInternal(string relativeUrl, string method, object parameters = null)
        {
            var serial = "";

            if (parameters != null)
                serial = JsonConvert.SerializeObject(parameters);

            var result = UploadJsonString(relativeUrl, method, serial);

            if (!string.IsNullOrEmpty(result))
            {
                var joResult = JObject.Parse(result);

                if (joResult["errors"] != null)
                {
                    var errorCollection = JArray.Parse(joResult["errors"].ToString());

                    throw new ArgumentException(errorCollection.First().ToString());
                }
            }

            return result;
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
