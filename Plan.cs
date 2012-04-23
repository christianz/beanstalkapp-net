using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeanstalkApp_Sharp
{
    public class Plan
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("repositoryies")]
        public int Repositories { get; set; }

        [JsonProperty("users")]
        public int Users { get; set; }

        [JsonProperty("servers")]
        public int Servers { get; set; }

        [JsonProperty("storage")]
        public int Storage { get; set; }

        public static IEnumerable<Plan> FindAll()
        {
            return Beanstalk.GetMany<Plan>("/plans.json");
        }
    }
}
