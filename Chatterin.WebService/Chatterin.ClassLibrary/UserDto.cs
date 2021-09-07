using System;
using System.Collections.Generic;
using System.Text;

namespace Chatterin.ClassLibrary
{
    public class UserDto : Resource
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
