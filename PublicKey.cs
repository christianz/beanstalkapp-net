using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper class for the PublicKey Resource - http://api.beanstalkapp.com/public_key.html
    /// </summary>
    public class PublicKey
    {
        /// <summary>
        /// Unique ID of the PublicKey.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the associated Account.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// ID of the User who owns the key.
        /// </summary>
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Human-readable name of the key.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Public SSH key.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Time when the Invitation was las updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the Invitation was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Find All Public Keys
        /// 
        /// Admins can pass user_id parameter to fetch keys for all account’s users. Otherwise only current user’s keys are returned.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PublicKey> FindAll()
        {
            return Beanstalk.GetMany<PublicKey>("/public_keys.json");
        }

        /// <summary>
        /// Find All Public Keys by user Id.
        /// 
        /// Admins can pass user_id parameter to fetch keys for all account’s users. Otherwise only current user’s keys are returned.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PublicKey> FindForUser(int userId)
        {
            return Beanstalk.GetMany<PublicKey>("/public_keys.json?user_id=" + userId);
        }

        /// <summary>
        /// Find Public Key by Id.
        /// 
        /// Admins can view other users’ keys. Regular users can view their own keys only.
        /// </summary>
        /// <param name="publicKeyId"></param>
        /// <returns></returns>
        public static PublicKey Find(int publicKeyId)
        {
            return Beanstalk.Get<PublicKey>("/public_keys/" + publicKeyId + ".json");
        }

        /// <summary>
        /// Create Public Key
        /// 
        /// Admins can pass user_id parameter to create a key for a specific user under that account, otherwise the key will be created for a currently logged in user.
        /// 
        /// Name is optional and will be derived from the key if possible. Otherwise it will be stubbed with a date stamp.
        /// </summary>
        public PublicKey Create()
        {
            return Beanstalk.Update<PublicKey>("/public_key.json", "POST", new
            {
                public_keys = new
                {
                    name = Name,
                    content = Content
                }
            });
        }

        /// <summary>
        /// Update Public Key
        /// 
        /// Admins can update other users’ keys. Regular users can update their own keys only.
        /// </summary>
        public PublicKey Save()
        {
            return Beanstalk.Update<PublicKey>("/public_key/" + Id + ".json", "PUT", new
            {
                public_keys = new
                {
                    name = Name,
                    content = Content
                }
            });
        }
        
        /// <summary>
        /// Delete Public Key
        /// 
        /// Admins can delete other users’ keys. Regular users can delete their own keys only.
        /// </summary>
        public void Delete()
        {
            Beanstalk.Update("/public_keys/" + Id + ".json", "DELETE");
        }
    }
}
