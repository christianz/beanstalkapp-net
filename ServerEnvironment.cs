using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class ServerEnvironment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("branch_name")]
        public string BranchName { get; set; }

        [JsonProperty("automatic")]
        public bool Automatic { get; set; }

        [JsonProperty("color_label")]
        public string ColorLabel { get; set; }

        [JsonProperty("current_version")]
        public string CurrentVersion { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<ServerEnvironment> FindAllForRepository(string repositoryId)
        {
            return Beanstalk.GetMany<ServerEnvironment>("/" + repositoryId + "/server_environments.json");
        }

        public static ServerEnvironment FindForRepository(int serverEnvironmentId, string repositoryId)
        {
            return Beanstalk.Get<ServerEnvironment>("/" + repositoryId + "/server_environments./" + serverEnvironmentId + ".json");
        }

        public void Create(string repositoryId)
        {
            Beanstalk.Update("/" + repositoryId + "/server_environments.json", "POST", new
            {
                server_environment = new
                {
                    name = Name,
                    automatic = Automatic,
                    branch_name = BranchName,
                    color_label = ColorLabel
                }
            });
        }

        public void Save()
        {
            Beanstalk.Update("/" + RepositoryId + "/server_environments/" + Id + ".json", "PUT", new
            {
                server_environment = new
                {
                    name = Name,
                    automatic = Automatic,
                    branch_name = BranchName,
                    color_label = ColorLabel
                }
            });
        }
    }
}
