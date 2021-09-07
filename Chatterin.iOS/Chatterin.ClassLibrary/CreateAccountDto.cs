using System;
namespace Chatterin.ClassLibrary
{
    public class CreateAccountDto : Resource
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
