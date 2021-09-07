using System;
using Chatterin.ClassLibrary;
using Chatterin.Services;
using Foundation;
using UIKit;

namespace Chatterin
{
    public partial class SenderTableViewCell : UITableViewCell, IMessageTableViewCell
    {
        public static readonly NSString Key = new NSString("SenderTableViewCell");
        public static readonly UINib Nib;

        static SenderTableViewCell()
        {
            Nib = UINib.FromName("SenderTableViewCell", NSBundle.MainBundle);
        }

        protected SenderTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void UpdateCell(MessageDto message)
        {
            _imageView.Image = ImageUtilityService.SenderBubble.Value;
            _imageView.ContentMode = UIViewContentMode.ScaleToFill;
            _text.Text = message.MessageTxt;
        }
    }
}
