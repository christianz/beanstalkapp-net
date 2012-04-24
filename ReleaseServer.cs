using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    public class ReleaseServer
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("server_environment_id")]
        public int ServerEnvironmentId { get; set; }

        [JsonProperty("environment_name")]
        public string EnvironmentName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("local_path")]
        public string LocalPath { get; set; }

        [JsonProperty("remote_path")]
        public string RemotePath { get; set; }

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("remote_addr")]
        public string RemoteAddr { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("shell_code")]
        public string ShellCode { get; set; }

        [JsonProperty("pre_release_hook")]
        public string PreReleaseHook { get; set; }

        [JsonProperty("post_release_hook")]
        public string PostReleaseHook { get; set; }

        [JsonProperty("use_feat")]
        public bool UseFeat { get; set; }

        [JsonProperty("use_active_mode")]
        public bool UseActiveMode { get; set; }

        [JsonProperty("parallel_uploading")]
        public bool ParallelUploading { get; set; }

        [JsonProperty("exclude_paths")]
        public string ExcludePaths { get; set; }

        [JsonProperty("authenticate_by_key")]
        public bool AuthenticateByKey { get; set; }

        [JsonProperty("public_key")]
        public string PublicKey { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<ReleaseServer> FindAllForEnvironment(int repositoryId, int environmentId)
        {
            return Beanstalk.GetMany<ReleaseServer>("/" + repositoryId + "/release_servers.json?environment_id=" + environmentId);
        }

        public static ReleaseServer Find(int repositoryId, int releaseServerId)
        {
            return Beanstalk.Get<ReleaseServer>("/" + repositoryId + "/release_servers/" + releaseServerId + ".json");
        }

        public void Create()
        {
            Beanstalk.Update("/" + RepositoryId + "/release_servers.json", "POST", new
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

        public void Save()
        {
            Beanstalk.Update("/" + RepositoryId + "/release_servers/" + Id + ".json", "PUT", new
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

        public void Delete()
        {
            Beanstalk.Update("/" + RepositoryId + "/release_servers/" + Id + ".json", "DELETE");
        }
    }
}
