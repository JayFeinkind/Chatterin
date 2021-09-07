using System;
using System.Collections.Generic;

namespace Chatterin.ClassLibrary
{
    public class UserConversationDto : Resource
    {
        public string UserName { get; set; }
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public bool Archived { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}
