using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime TokenExpiredUtc { get; set; }

        public virtual User User { get; set; }
    }
}
