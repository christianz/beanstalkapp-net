using System.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace beanstalkapp_net
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        [JsonProperty("author_id")]
        public int AuthorId { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        [JsonProperty("author_login")]
        public string AuthorLogin { get; set; }

        [JsonProperty("revision")]
        public string Revision { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("line_number")]
        public int LineNumber { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("rendered_body")]
        public string RenderedBody { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        public static IEnumerable<Comment> FindAllCommentsForRepository(int repositoryId)
        {
            return Beanstalk.GetMany<Comment>("/" + repositoryId + " /comments.json");
        }

        public static IEnumerable<Comment> FindAllCommentsForChangeset(int repositoryId, string revision)
        {
            return Beanstalk.GetMany<Comment>("/" + repositoryId + "/comments.json?revision=" + revision);
        }

        public static IEnumerable<Comment> FindAllCommentsForUser(int userId)
        {
            return Beanstalk.GetMany<Comment>("/user.json?user_id=" + userId);
        }

        public static Comment Find(int repositoryId, int commentId)
        {
            return Beanstalk.Get<Comment>("/" + repositoryId + "/comments/" + commentId + ".json");
        }

        public void Create()
        {
            Beanstalk.Update("/" + RepositoryId + "/comments.json", "POST", new
                                                                                {
                                                                                    comment = new
                                                                                                  {
                                                                                                      body = Body,
                                                                                                      revision = Revision,
                                                                                                      file_path = FilePath,
                                                                                                      line_number = LineNumber
                                                                                                  }
                                                                                });
        }
    }
}