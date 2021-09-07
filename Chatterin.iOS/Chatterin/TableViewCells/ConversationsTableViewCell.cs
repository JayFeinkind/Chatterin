using System;
using Chatterin.ClassLibrary;
using Foundation;
using UIKit;

namespace Chatterin
{
    public partial class ConversationsTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ConversationsTableViewCell");
        public static readonly UINib Nib;

        static ConversationsTableViewCell()
        {
            Nib = UINib.FromName("ConversationsTableViewCell", NSBundle.MainBundle);
        }

        protected ConversationsTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void UpdateCell(ConversationDto dto, int currentUserId)
        {
            _header.Text = dto.GetUserNameStr(currentUserId);
            _detail.Text = dto.GetLastMessageText();
            _timeStamp.Text = dto.GetLastMessageSent();
        }
    }
}
