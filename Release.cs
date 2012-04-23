using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeanstalkApp_Sharp
{
    public class Release
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("environment_id")]
        public int EnvironmentId { get; set; }

        [JsonProperty("environment_name")]
        public string EnvironmentName { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("deployed_at")]
        public DateTime DeployedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<Release> FindAllForAccount()
        {
            return Beanstalk.GetMany<Release>("/releases.json");
        }

        public static IEnumerable<Release> FindAllForRepository(int repositoryId)
        {
            return Beanstalk.GetMany<Release>("/" + repositoryId + "/releases.json");
        }
    }
}
