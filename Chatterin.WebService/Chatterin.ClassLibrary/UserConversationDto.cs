using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class UserConversationDto : Resource
    {
        public int ConversationId { get; set; }
        public int UserId { get; set; }
        public bool Archived { get; set; }
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}
