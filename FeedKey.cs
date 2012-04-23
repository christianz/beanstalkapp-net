using System;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class FeedKey
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static FeedKey FindForCurrent()
        {
            return Beanstalk.Get<FeedKey>("/feed_key.json");
        }
    }
}
