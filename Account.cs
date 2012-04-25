using System;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper for the Account Resource - http://api.beanstalkapp.com/account.html
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Unique ID of the Account
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the User who owns the Account.
        /// </summary>
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        /// <summary>
        /// ID of the associated Plan.
        /// </summary>
        [JsonProperty("plan_id")]
        public int PlanId { get; set; }

        /// <summary>
        /// Human-readable name. Usually company name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Sub-domain.
        /// </summary>
        [JsonProperty("third_level_domain")]
        public string ThirdLevelDomain { get; set; }

        /// <summary>
        /// Time zone that was selected during Sign Up.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// True if account was suspended due to failed monthly payment.
        /// </summary>
        [JsonProperty("suspended")]
        public bool Suspended { get; set; }

        /// <summary>
        /// Time when the integration was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the account was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Returns the current account.
        /// </summary>
        /// <returns></returns>
        public static Account Find()
        {
            return Beanstalk.Get<Account>("/account.json");
        }

        /// <summary>
        /// Updates the following attributes:
        /// 
        /// * name
        /// * time_zone
        /// </summary>
        public void Save()
        {
            Beanstalk.Update("/account.json", "PUT", new
            {
                account = new
                {
                    name = Name,
                    time_zone = TimeZone
                }
            });
        }
    }
}
