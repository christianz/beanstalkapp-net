using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class Permission
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("server_environment_id")]
        public int ServerEnvironmentId { get; set; }

        [JsonProperty("full_deployments_access")]
        public bool FullDeploymentsAccess { get; set; }

        [JsonProperty("read")]
        public bool Read { get; set; }

        [JsonProperty("write")]
        public bool Write { get; set; }

        public static IEnumerable<Permission> FindAllForUser(int userId)
        {
            return Beanstalk.GetMany<Permission>("/permissions/" + userId + ".json");
        }
        
        public void Create()
        {
            Beanstalk.Update("/permissions.json", "POST", new
            {
                permission = new
                {
                    user_id = UserId,
                    repository_id = RepositoryId,
                    write = Write,
                    server_environment_id = ServerEnvironmentId,
                    full_deployments_access = FullDeploymentsAccess,
                    read = Read
                }
            });
        }

        public void Delete()
        {
            Beanstalk.Update("/permissions/" + Id + ".json", "DELETE");
        }
    }
}
