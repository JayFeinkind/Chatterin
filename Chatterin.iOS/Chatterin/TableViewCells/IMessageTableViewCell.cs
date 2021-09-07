using System;
using Chatterin.ClassLibrary;

namespace Chatterin
{
    public interface IMessageTableViewCell
    {
        void UpdateCell(MessageDto message);
    }
}
