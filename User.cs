using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper for the User Resource - http://api.beanstalkapp.com/user.html
    /// </summary>
    public class User
    {
        /// <summary>
        /// Unique ID of the User.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the Account user belongs to.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// Username. Unique per Account.
        /// </summary>
        [JsonProperty("login")]
        public string Login { get; set; }

        /// <summary>
        /// Email address. Unique per account.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// True if User has created the Account initially.
        /// </summary>
        [JsonProperty("owner")]
        public bool Owner { get; set; }

        /// <summary>
        /// True if has admin privileges in the Account.
        /// </summary>
        [JsonProperty("admin")]
        public bool? Admin { get; set; }

        /// <summary>
        /// User’s preferred time zone. If not specified, Account’s time zone is used by default.
        /// </summary>
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Time when the User was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the User was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The user's password. Only used when creating or updating a user.
        /// </summary>
        public string Password { private get; set; }

        /// <summary>
        /// Find all users
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<User> FindAll()
        {
            return Beanstalk.GetMany<User>("/users.json");
        }

        /// <summary>
        /// Find user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User Find(int id)
        {
            return Beanstalk.Get<User>("/users/" + id + ".json");
        }
        /// <summary>
        /// Find user for current session
        /// </summary>
        /// <returns></returns>
        public static User FindCurrent()
        {
            return Beanstalk.Get<User>("/users/current.json");
        }

        /// <summary>
        /// Creates a new User and sets the following attributes:
        /// 
        /// * login
        /// * email
        /// * first_name
        /// * last_name
        /// * password
        /// * admin
        /// * timezone
        /// </summary>
        public User Create()
        {
            if (string.IsNullOrEmpty(Login))
                throw new Exception("Login can't be blank.");

            return Beanstalk.Update<User>("/users.json", "POST", new
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

        /// <summary>
        /// Updates a User, setting the following attributes:
        /// 
        /// * login
        /// * email
        /// * first_name
        /// * last_name
        /// * password
        /// * admin
        /// * timezone
        /// </summary>
        public User Save()
        {
            return Beanstalk.Update<User>("/users.json", "PUT", new
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

        /// <summary>
        /// Deletes this user.
        /// </summary>
        public void Delete()
        {
            Beanstalk.Update("/users/" + Id + ".json", "DELETE");
        }
    }
}
