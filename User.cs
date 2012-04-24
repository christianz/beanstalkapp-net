using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
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

        public string Password { private get; set; }

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

        public void Create()
        {
            Beanstalk.Update("/users.json", "POST", new
            {
                user = new
                {
                    admin = Admin,
                    timezone = TimeZone,
                    last_name = LastName,
                    login = Login,
                    first_name = FirstName,
                    email = Email,
                    password = Password
                }
            });
        }

        public void Save()
        {
            Beanstalk.Update("/users.json", "PUT", new
            {
                user = new
                {
                    admin = Admin,
                    timezone = TimeZone,
                    last_name = LastName,
                    login = Login,
                    first_name = FirstName,
                    email = Email,
                    password = Password
                }
            });
        }

        public void Delete()
        {
            Beanstalk.Update("/users/" + Id + ".json", "DELETE");
        }
    }
}
