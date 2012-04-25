using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper class for the Comment Resource - http://api.beanstalkapp.com/comment.html
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Unique UD of Comment.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the associated Account.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// ID of the associated Repository.
        /// </summary>
        [JsonProperty("repository_id")]
        public int RepositoryId { get; set; }

        /// <summary>
        /// ID of the User who created the Comment.
        /// </summary>
        [JsonProperty("author_id")]
        public int AuthorId { get; set; }

        /// <summary>
        /// Full name of the User who created the comment.
        /// </summary>
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Email address of the User who created the comment.
        /// </summary>
        [JsonProperty("author_email")]
        public string AuthorEmail { get; set; }

        /// <summary>
        /// Username of the User who created the comment.
        /// </summary>
        [JsonProperty("author_login")]
        public string AuthorLogin { get; set; }

        /// <summary>
        /// Revision number (or hash ID) of the commit for which Comment was created.
        /// </summary>
        [JsonProperty("revision")]
        public string Revision { get; set; }

        /// <summary>
        /// Path of the file for which Comment was created (one of the commit’s changed files).
        /// </summary>
        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        /// <summary>
        /// Line number of the file for which Comment was created.
        /// </summary>
        [JsonProperty("line_number")]
        public int LineNumber { get; set; }

        /// <summary>
        /// Contents of the Comment.
        /// </summary>
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Contents of the Comment with Textile and HTML rendering applied. Generated automatically.
        /// </summary>
        [JsonProperty("rendered_body")]
        public string RenderedBody { get; set; }

        /// <summary>
        /// Time when the Comment was las updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the Comment was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Find All Comments for Repository
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> FindAllCommentsForRepository(int repositoryId)
        {
            return Beanstalk.GetMany<Comment>("/" + repositoryId + " /comments.json");
        }

        /// <summary>
        /// Find All Comments for Changeset
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="revision"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> FindAllCommentsForChangeset(int repositoryId, string revision)
        {
            return Beanstalk.GetMany<Comment>("/" + repositoryId + "/comments.json?revision=" + revision);
        }

        /// <summary>
        /// Find All Comments for User
        /// 
        /// Returns an array of comments by a specific user across all repositories.
        /// 
        /// Admins can use user_id parameter to specify ID of any user from the account. Regular users can use method without user_id to receive their comments only.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IEnumerable<Comment> FindAllCommentsForUser(int userId)
        {
            return Beanstalk.GetMany<Comment>("/user.json?user_id=" + userId);
        }

        /// <summary>
        /// Find Comment
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public static Comment Find(int repositoryId, int commentId)
        {
            return Beanstalk.Get<Comment>("/" + repositoryId + "/comments/" + commentId + ".json");
        }

        /// <summary>
        /// Create Comment
        /// </summary>
        public Comment Create()
        {
            return Beanstalk.Update<Comment>("/" + RepositoryId + "/comments.json", "POST", new
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