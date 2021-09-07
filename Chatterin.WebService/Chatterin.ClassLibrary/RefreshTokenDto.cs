using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class RefreshTokenDto
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiredUtc { get; set; }
    }
}
