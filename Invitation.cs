using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeanstalkApp_Sharp
{
    public class Invitation
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        [JsonProperty("creator_name")]
        public string CreatorName { get; set; }

        [JsonProperty("creator_email")]
        public string CreatorEmail { get; set; }

        [JsonProperty("secure_token")]
        public string SecureToken { get; set; }

        [JsonProperty("signup_url")]
        public string SignupUrl { get; set;}

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<Invitation> FindAll()
        {
            return Beanstalk.GetMany<Invitation>("/invitations.json");
        }

        public static Invitation Find(int id)
        {
            return Beanstalk.Get<Invitation>("/invitations/" + id + ".json");
        }
    }
}
