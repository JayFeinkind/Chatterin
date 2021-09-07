using System;
using System.Collections.Generic;

namespace Chatterin.ClassLibrary
{
    public class TokenDto
    {
        public string ClientId { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public Dictionary<string, string> TokenParams { get; set; }
    }

    public static class TokenTypes
    {
        public static readonly string JWT = "JWT";
        public static readonly string Refresh = "Refresh";
    }
}
