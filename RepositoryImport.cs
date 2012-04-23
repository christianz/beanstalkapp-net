using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class RepositoryImport
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<RepositoryImport> FindAll()
        {
            return Beanstalk.GetMany<RepositoryImport>("/repository_imports.json");
        }

        public static RepositoryImport Find(int id)
        {
            return Beanstalk.Get<RepositoryImport>("/repository_imports/" + id + ".json");
        }

        public void Create()
        {
            Beanstalk.Upload("/" + RepositoryId + "/repository_imports.json", "POST", new
            {
                repository_import = new
                {
                    uri = Uri
                }
            });
        }
    }
}
