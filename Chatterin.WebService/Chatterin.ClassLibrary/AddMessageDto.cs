using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class AddMessageDto
    {
        public string Text { get; set; }
        public int UserConversationId { get; set; }
        public DateTime SentUtc { get; set; }
    }
}
