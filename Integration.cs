using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class Integration
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("deactivated_at")]
        public DateTime DeactivatedAt { get; set; }

        [JsonProperty("activated_at")]
        public DateTime ActivatedAt { get; set; }

        [JsonProperty("activated_by_user_id")]
        public int ActivatedByUserId { get; set; }

        [JsonProperty("deactivated_by_user_id")]
        public int DeactivatedByUserId { get; set; }
    }
}
