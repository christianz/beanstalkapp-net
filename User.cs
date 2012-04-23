using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace BeanstalkApp_Sharp
{
    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("owner")]
        public bool Owner { get; set; }

        [JsonProperty("admin")]
        public bool? Admin { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<User> FindAll()
        {
            return Beanstalk.GetMany<User>("/users.json");
        }

        public static User Find(int id)
        {
            return Beanstalk.Get<User>("/users/" + id + ".json");
        }

        public static User FindCurrent()
        {
            return Beanstalk.Get<User>("/users/current.json");
        }
    }
}
