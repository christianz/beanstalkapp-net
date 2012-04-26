using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper class for the Permission Resource - http://api.beanstalkapp.com/permissions.html
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// Unique ID of the Permission.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the Repository to which permissions are granted.
        /// </summary>
        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        /// <summary>
        /// ID of the User to whom permissions have been granted.
        /// </summary>
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// ID of ServerEnvironment to which User should have access.
        /// 
        /// If specified, allows user to create and retry deployments.
        /// 
        /// Does not allow viewing incidents or changing any deployments settings.
        /// 
        /// Can not be set together with full_deployments_acccess. Automatically sets read to true.
        /// </summary>
        [JsonProperty("server_environment_id")]
        public int ServerEnvironmentId { get; set; }

        /// <summary>
        /// True if allow full access to Repository’s deployments.
        /// 
        /// Includes: managing environments and servers; viewing incidents; creating and retrying deployments.
        /// 
        /// Automatically sets read to true.
        /// </summary>
        [JsonProperty("full_deployments_access")]
        public bool FullDeploymentsAccess { get; set; }

        /// <summary>
        /// True if allow read-access to Repository.
        /// </summary>
        [JsonProperty("read")]
        public bool Read { get; set; }

        /// <summary>
        /// True if allow read and write access to Repository.
        /// </summary>
        [JsonProperty("write")]
        public bool Write { get; set; }

        /// <summary>
        /// Find Permissions for user
        /// 
        /// Admins can view permissions for any user of the account. Regular users can find their permissions only.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<Permission> FindAllForUser(int userId)
        {
            return Beanstalk.GetMany<Permission>("/permissions/" + userId + ".json");
        }
        
        /// <summary>
        /// Create Permissions
        /// 
        /// Admin privileges required for this API method.
        /// 
        /// With this method you can specify user’s permissions for a specific repository.
        /// 
        /// You can specify as many or as little attributes as you need. This method will reset any existing permissions user might have before creating new ones.
        /// </summary>
        public Permission Create()
        {
            return Beanstalk.Update<Permission>("/permissions.json", "POST", new
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

        /// <summary>
        /// Delete Permission
        /// 
        /// Admin privileges required for this API method.
        /// 
        /// This method allows you to remove any permissions for a specified user in a specified repository.
        /// </summary>
        public void Delete()
        {
            Beanstalk.Update("/permissions/" + Id + ".json", "DELETE");
        }
    }
}
