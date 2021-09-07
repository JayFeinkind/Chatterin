using System;
using Chatterin.ClassLibrary;
using Chatterin.Services;
using Foundation;
using UIKit;

namespace Chatterin
{
    public partial class ReceiverTableViewCell : UITableViewCell, IMessageTableViewCell
    {
        public static readonly NSString Key = new NSString("ReceiverTableViewCell");
        public static readonly UINib Nib;

        static ReceiverTableViewCell()
        {
            Nib = UINib.FromName("ReceiverTableViewCell", NSBundle.MainBundle);
        }

        protected ReceiverTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void UpdateCell(MessageDto message)
        {
            _imageView.Image = ImageUtilityService.ReceiverBubble.Value;
            _imageView.ContentMode = UIViewContentMode.ScaleToFill;
            _text.Text = message.MessageTxt;
        }
    }
}
