using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class Repository
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color_label")]
        public string ColorLabel { get; set; }

        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("vcs")]
        public string Vcs { get; set; }

        [JsonProperty("repository_url")]
        public string RepositoryUrl { get; set; }

        [JsonProperty("last_commit_at")]
        public DateTime LastCommitAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<Repository> FindAll()
        {
            return Beanstalk.GetMany<Repository>("/repositories.json");
        }

        public static Repository Find(int id)
        {
            return Beanstalk.Get<Repository>("/repositories/" + id + ".json");
        }

        public void Create(bool createStructure)
        {
            Beanstalk.Upload("/repositories.json", "POST", new
            {
                repository = new
                {
                    type_id = Vcs,
                    name = Name,
                    title = Title,
                    color_label = ColorLabel,
                    default_branch = DefaultBranch,
                    create_structure = createStructure
                }
            });
        }

        public void Save()
        {
            Beanstalk.Upload("/repositories.json", "PUT", new
            {
                repository = new
                {
                    type_id = Vcs,
                    name = Name,
                    title = Title,
                    color_label = ColorLabel,
                    default_branch = DefaultBranch
                }
            });
        }
    }
}
