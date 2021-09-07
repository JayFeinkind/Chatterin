using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Chatterin.ClassLibrary
{
    public class Conversation : Resource
    {
        public DateTime CreatedUtc { get; set; }

        [Ignore]
        public IEnumerable<UserConversation> UserConversations { get; set; }
    }
}
