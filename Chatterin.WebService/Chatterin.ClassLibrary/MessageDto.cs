﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class MessageDto : Resource
    {
        public int UserConversationId { get; set; }
        public string MessageTxt { get; set; }
        public DateTime SentUtc { get; set; }
    }
}
