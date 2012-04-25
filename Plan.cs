using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper for the Plan Resource - http://api.beanstalkapp.com/plan.html
    /// </summary>
    public class Plan
    {
        /// <summary>
        /// Unique ID of the Plan.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Human-readable name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Monthly price in dollars.
        /// </summary>
        [JsonProperty("price")]
        public int Price { get; set; }

        /// <summary>
        /// Max number of repositories allowed on this Plan.
        /// </summary>
        [JsonProperty("repositoryies")]
        public int Repositories { get; set; }

        /// <summary>
        /// Max number of users allowed on this Plan.
        /// </summary>
        [JsonProperty("users")]
        public int Users { get; set; }

        /// <summary>
        /// Max number of servers allowed per repository on this Plan.
        /// </summary>
        [JsonProperty("servers")]
        public int Servers { get; set; }

        /// <summary>
        /// Storage capacity of this Plan in Megabytes.
        /// </summary>
        [JsonProperty("storage")]
        public int Storage { get; set; }

        /// <summary>
        /// Find all plans
        /// </summary>
        /// <returns>a list of the possible plans</returns>
        public static IEnumerable<Plan> FindAll()
        {
            return Beanstalk.GetMany<Plan>("/plans.json");
        }
    }
}
