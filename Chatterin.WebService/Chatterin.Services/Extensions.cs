using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Chatterin.Services
{
    public static class Extensions
    {
        //TODO this does not seem like the best way to validate email address
        public static bool IsValidEmail(this string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
