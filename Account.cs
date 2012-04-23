using System;
using Newtonsoft.Json;

namespace BeanstalkApp_Sharp
{
    public class Account
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("plan_id")]
        public int PlanId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("third_level_domain")]
        public string ThirdLevelDomain { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static Account Find()
        {
            return Beanstalk.Get<Account>("/account.json");
        }
    }
}
