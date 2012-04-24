﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
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
            var json = JObject.Parse(GetJson(relativeUrl));
            var str = json.First.First.ToString();

            return JsonConvert.DeserializeObject<T>(str);
        }

        public static IEnumerable<T> GetMany<T>(string relativeUrl)
        {
            var array = JArray.Parse(GetJson(relativeUrl));

            return array.Select(obj => obj.First.First.ToString()).Select(JsonConvert.DeserializeObject<T>);
        }

        private static string GetJson(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
                throw new Exception("Relative URL must start with '/'.");

            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                wc.Credentials = new NetworkCredential(Username, Password);

                var s = wc.DownloadString(ApiUrl + relativeUrl);
                return s;
            }
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

            using (var wc = new WebClient())
            {
                wc.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                wc.Credentials = new NetworkCredential(Username, Password);

                wc.UploadString(ApiUrl + relativeUrl, method, serial);
            }
        }
    }
}
