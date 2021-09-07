using System;
using System.Collections.Generic;

namespace Chatterin.ClassLibrary
{
    public partial class User : Resource
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
            UserConversations = new HashSet<UserConversation>();
        }
        
        public string UserName { get; set; }
        public string EmailAddress { get; set; }

        public virtual Membership Membership { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<UserConversation> UserConversations { get; set; }
    }
}
