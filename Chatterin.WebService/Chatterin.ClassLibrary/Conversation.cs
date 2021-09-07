using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public partial class Conversation : Resource
    {
        public Conversation()
        {
            UserConversations = new HashSet<UserConversation>();
        }

        public DateTime CreatedUtc { get; set; }

        public virtual ICollection<UserConversation> UserConversations { get; set; }
    }
}
