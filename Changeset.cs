using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class Changeset
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("hash_id")]
        public string HashId { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("changed_files")]
        public string ChangedFiles { get; set; }

        [JsonProperty("changed_dirs")]
        public string ChangedDirs { get; set; }

        [JsonProperty("changed_properties")]
        public string ChangedProperties { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("too_large")]
        public bool TooLarge { get; set; }

        public static IEnumerable<Changeset> FindAll()
        {
            return Beanstalk.GetMany<Changeset>("/changesets.json");
        }

        public static IEnumerable<Changeset> FindAllForRepository(int repositoryId)
        {
            return Beanstalk.GetMany<Changeset>("/changesets/repository.json?repository_id=" + repositoryId);
        }

        public static Changeset FindForRepository(int revision, string repositoryId)
        {
            return Beanstalk.Get<Changeset>("/changesets/" + revision + ".json?repository_id=" + repositoryId);
        }
    }
}
