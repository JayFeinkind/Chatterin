using System;
namespace Chatterin.ClassLibrary
{
    public class MessageDto
    {
        public string MessageTxt { get; set; }
        public DateTime SentUtc { get; set; }
        public bool SentByUser { get; set; }
        public string SentByName { get; set; }
    }
}
