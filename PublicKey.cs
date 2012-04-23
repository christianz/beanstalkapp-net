using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BeanstalkApp_Sharp
{
    public class PublicKey
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<PublicKey> FindForUser(int userId = -1)
        {
            var url = "/public_keys";

            if (userId != -1)
                url += "?user_id=" + userId;

            return Beanstalk.GetMany<PublicKey>(url + ".json");
        }

        public static PublicKey Find(int publicKeyId)
        {
            return Beanstalk.Get<PublicKey>("/public_keys/" + publicKeyId + ".json");
        }
    }
}
