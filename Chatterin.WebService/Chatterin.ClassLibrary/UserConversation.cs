using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public partial class UserConversation : Resource
    {
        public UserConversation()
        {
            Messages = new HashSet<Message>();
        }

        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public bool Archived { get; set; }

        public virtual Conversation Conversation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
