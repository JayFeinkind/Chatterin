using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class ConversationDto : Resource
    {
        public DateTime CreatedUtc { get; set; }

        public IEnumerable<UserConversationDto> UserConversations { get; set; }
    }
}
