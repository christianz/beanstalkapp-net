using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper class for the Release Server Resource - http://api.beanstalkapp.com/release_server.html
    /// </summary>
    public class ReleaseServer
    {
        /// <summary>
        /// Unique ID of Server.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the associated Account.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// ID of the repository that Server is deploying.
        /// </summary>
        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        /// <summary>
        /// ID of ServerEnvironment to which Server belongs.
        /// </summary>
        [JsonProperty("server_environment_id")]
        public int ServerEnvironmentId { get; set; }

        /// <summary>
        /// Human-readable name of the associated ServerEnvironment.
        /// </summary>
        [JsonProperty("environment_name")]
        public string EnvironmentName { get; set; }

        /// <summary>
        /// Human-readable name of Server.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Protocol that should be use for deployments. Can be ftp, sftp or shell.
        /// </summary>
        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        /// <summary>
        /// A path in Repository that should be deployed.
        /// </summary>
        [JsonProperty("local_path")]
        public string LocalPath { get; set; }

        /// <summary>
        /// A path on remote server to which files should be deployed.
        /// </summary>
        [JsonProperty("remote_path")]
        public string RemotePath { get; set; }

        /// <summary>
        /// A port on remote server to which Server should connect to deploy.
        /// </summary>
        [JsonProperty("port")]
        public int Port { get; set; }

        /// <summary>
        /// An IP address (or URL) to which Server should connect to deploy.
        /// </summary>
        [JsonProperty("remote_addr")]
        public string RemoteAddr { get; set; }

        /// <summary>
        /// A revision number (or hash ID) of the last commit deployed to the Server.
        /// </summary>
        [JsonProperty("revision")]
        public string Revision { get; set; }

        /// <summary>
        /// Shell code that will be executed on the remote server during deploy. SSH deployments only.
        /// </summary>
        [JsonProperty("shell_code")]
        public string ShellCode { get; set; }

        /// <summary>
        /// A URL to which POST request with information will be sent at the beginning of every deployment.
        /// </summary>
        [JsonProperty("pre_release_hook")]
        public string PreReleaseHook { get; set; }

        /// <summary>
        /// A URL to which POST request with information will be sent at the end of every deployment.
        /// </summary>
        [JsonProperty("post_release_hook")]
        public string PostReleaseHook { get; set; }

        /// <summary>
        /// Use FEAT FTP command when communicating with the remote server. FTP deployments only.
        /// </summary>
        [JsonProperty("use_feat")]
        public bool UseFeat { get; set; }

        /// <summary>
        /// Use Active Mode when communicating with the remote server. FTP deployments only.
        /// </summary>
        [JsonProperty("use_active_mode")]
        public bool UseActiveMode { get; set; }

        /// <summary>
        /// Upload files in multiple threads to the remote server. FTP and SFTP deployments only.
        /// </summary>
        [JsonProperty("parallel_uploading")]
        public bool ParallelUploading { get; set; }

        /// <summary>
        /// A list of paths from Repository that should be ignored during deployments. Multiple paths should be separated with new line characters.
        /// </summary>
        [JsonProperty("exclude_paths")]
        public string ExcludePaths { get; set; }

        /// <summary>
        /// Use SSH key authentication instead of login & password.
        /// </summary>
        [JsonProperty("authenticate_by_key")]
        public bool AuthenticateByKey { get; set; }

        /// <summary>
        /// Unique public SSH key of the server. Can be installed on the remote server to allow authentication by key.
        /// </summary>
        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        /// <summary>
        /// Time when the server was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the server was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Find All Release Servers
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="environmentId"></param>
        /// <returns></returns>
        public static IEnumerable<ReleaseServer> FindAllForEnvironment(int repositoryId, int environmentId)
        {
            return Beanstalk.GetMany<ReleaseServer>("/" + repositoryId + "/release_servers.json?environment_id=" + environmentId);
        }

        /// <summary>
        /// Find Release Server
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="releaseServerId"></param>
        /// <returns></returns>
        public static ReleaseServer Find(int repositoryId, int releaseServerId)
        {
            return Beanstalk.Get<ReleaseServer>("/" + repositoryId + "/release_servers/" + releaseServerId + ".json");
        }

        /// <summary>
        /// Create Release Server
        /// </summary>
        /// <returns></returns>
        public ReleaseServer Create()
        {
            return Beanstalk.Update<ReleaseServer>("/" + RepositoryId + "/release_servers.json", "POST", new
            {
                release_server = new
                {
                    name = Name,
                    local_path = LocalPath,
                    remote_path = RemotePath,
                    remote_addr = RemoteAddr,
                    protocol = Protocol,
                    port = Port,
                }
            });
        }

        /// <summary>
        /// Update Release Server
        /// </summary>
        /// <returns></returns>
        public ReleaseServer Save()
        {
            return Beanstalk.Update<ReleaseServer>("/" + RepositoryId + "/release_servers/" + Id + ".json", "PUT", new
            {
                release_server = new
                {
                    name = Name,
                    local_path = LocalPath,
                    remote_path = RemotePath,
                    remote_addr = RemoteAddr,
                    protocol = Protocol,
                    port = Port,
                }
            });
        }

        /// <summary>
        /// Delete Release Server
        /// </summary>
        public void Delete()
        {
            Beanstalk.Update("/" + RepositoryId + "/release_servers/" + Id + ".json", "DELETE");
        }
    }
}
