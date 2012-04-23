using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace beanstalkapp_net
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

        public static IEnumerable<PublicKey> FindAll()
        {
            return Beanstalk.GetMany<PublicKey>("/public_keys.json");
        }

        public static IEnumerable<PublicKey> FindForUser(int userId)
        {
            return Beanstalk.GetMany<PublicKey>("/public_keys.json?user_id=" + userId);
        }

        public static PublicKey Find(int publicKeyId)
        {
            return Beanstalk.Get<PublicKey>("/public_keys/" + publicKeyId + ".json");
        }

        public void Create()
        {
            Beanstalk.Upload("/public_key.json", "POST", new
            {
                public_keys = new
                {
                    name = Name,
                    content = Content
                }
            });
        }

        public void Save()
        {
            Beanstalk.Upload("/public_key/" + Id + ".json", "PUT", new
            {
                public_keys = new
                {
                    name = Name,
                    content = Content
                }
            });
        }

        public void Delete()
        {
            Beanstalk.Upload("/public_keys/" + Id + ".json", "DELETE", null);
        }
    }
}
