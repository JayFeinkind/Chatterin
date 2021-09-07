using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatterin.ClassLibrary
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string JWTTimeoutHours { get; set; }
        public string RefreshTimeoutHours { get; set; }
    }
}
