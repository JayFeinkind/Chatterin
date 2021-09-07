using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class TokenDto
    {
        public string ClientId { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public Dictionary<string, string> TokenParams { get; set; }
    }
}
