using System;
namespace Chatterin.ClassLibrary
{
    public partial class Membership : Resource
    {
        public byte[] Salt { get; set; }
        public byte[] Password { get; set; }

        public virtual User User { get; set; }
    }
}
