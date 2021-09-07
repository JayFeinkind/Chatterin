using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Chatterin.ClassLibrary
{
    public class UserConversation : Resource
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public bool Archived { get; set; }

        [Ignore]
        public IEnumerable<Message> Messages { get; set; }
    }
}
