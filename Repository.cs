using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper class for the Repository Resource - http://api.beanstalkapp.com/repository.html
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Unique ID.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the associated account.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }
        
        /// <summary>
        /// Human-readable name.
        /// 
        /// Required on create.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// File-system name.
        /// 
        /// Required on create.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the specified color label. See a list of available labels below.
        /// </summary>
        /// <seealso cref="ColorLabel"/>
        [JsonProperty("color_label")]
        public string ColorLabel { get; set; }

        /// <summary>
        /// Branch that is used by default when creating a new clone. Git only.
        /// </summary>
        [JsonProperty("default_branch")]
        public string DefaultBranch { get; set; }

        /// <summary>
        /// Type of the repository. Read-only.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; private set; }

        /// <summary>
        /// Name of a version control system the repoistory is using. See a list of available types below.
        /// </summary>
        /// <seealso cref="RepositoryType"/>
        [JsonProperty("vcs")]
        public string Vcs { get; set; }

        /// <summary>
        /// A URL that can be used to checkout or clone the repository.
        /// </summary>
        [JsonProperty("repository_url")]
        public string RepositoryUrl { get; set; }

        /// <summary>
        /// Time of the last commit.
        /// </summary>
        [JsonProperty("last_commit_at")]
        public DateTime? LastCommitAt { get; set; }

        /// <summary>
        /// Time when the repository was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the repository was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Find All Repositories
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Repository> FindAll()
        {
            return Beanstalk.GetMany<Repository>("/repositories.json");
        }

        /// <summary>
        /// Find Repository by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Repository Find(int id)
        {
            return Beanstalk.Get<Repository>("/repositories/" + id + ".json");
        }

        public IEnumerable<string> GetBranches()
        {
            return Beanstalk.GetMany<string>("/repositories/" + Id + "/branches.json");
        }

        public Repository Create(bool createStructure)
        {
            return Beanstalk.Update<Repository>("/repositories.json", "POST", new
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

        public Repository Save()
        {
            return Beanstalk.Update<Repository>("/repositories.json", "PUT", new
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
