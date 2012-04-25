using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace beanstalkapp_net
{
    /// <summary>
    /// Wrapper for the Invitation resource - http://api.beanstalkapp.com/invitation.html
    /// </summary>
    public class Invitation
    {
        /// <summary>
        /// Unique ID of the Invitation.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// ID of the Account invitation belongs to.
        /// </summary>
        [JsonProperty("account_id")]
        public int AccountId { get; set; }

        /// <summary>
        /// ID of the User created the Invitation.
        /// </summary>
        [JsonProperty("creator_id")]
        public int CreatorId { get; set; }

        /// <summary>
        /// Name of the User created the Invitation.
        /// </summary>
        [JsonProperty("creator_name")]
        public string CreatorName { get; set; }

        /// <summary>
        /// Email address of the User created the Invitation.
        /// </summary>
        [JsonProperty("creator_email")]
        public string CreatorEmail { get; set; }

        /// <summary>
        /// Unique secure key that is used by a signup_url.
        /// </summary>
        [JsonProperty("secure_token")]
        public string SecureToken { get; set; }

        /// <summary>
        /// URL that can be used to finalize the invitation.
        /// </summary>
        [JsonProperty("signup_url")]
        public string SignupUrl { get; set;}

        /// <summary>
        /// Time when the Invitation was last updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Time when the Invitation was first added to the system.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Find all invitations
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Invitation> FindAll()
        {
            return Beanstalk.GetMany<Invitation>("/invitations.json");
        }

        /// <summary>
        /// Find Invitation by ID
        /// </summary>
        /// <param name="invitationId"></param>
        /// <returns></returns>
        public static Invitation Find(int invitationId)
        {
            return Beanstalk.Get<Invitation>("/invitations/" + invitationId + ".json");
        }

        /// <summary>
        /// This method will create both User and Invitation for him. A usual invitation email with signup link will be delivered to the User.
        /// </summary>
        /// <seealso cref="User"/>
        /// <param name="user">An (unsaved) user instance to create.</param>
        public static Invitation CreateForUser(User user)
        {
            return Beanstalk.Update<Invitation>("/invitations.json", "POST", new
            {
                invitation = new
                {
                    user = new
                    {
                        email = user.Email,
                        first_name = user.FirstName,
                        last_name = user.LastName
                    }
                }
            });
        }

        public Invitation Resend(int userId)
        {
            return Beanstalk.Update<Invitation>("/invitations/resend/" + userId + ".json", "PUT");
        }
    }
}
