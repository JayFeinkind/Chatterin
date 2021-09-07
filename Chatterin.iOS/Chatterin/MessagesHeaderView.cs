using Foundation;
using ObjCRuntime;
using System;
using UIKit;

namespace Chatterin
{
    public partial class MessagesHeaderView : UIView
    {
        public MessagesHeaderView (IntPtr handle) : base (handle)
        {
        }

        public static MessagesHeaderView Create()
        {
            var nib = NSBundle.MainBundle.LoadNib("MessagesHeaderView", null, null);
            var view = Runtime.GetNSObject<MessagesHeaderView>(nib.ValueAt(0));

            return view;
        }

        public void UpdateView(string header)
        {
            _header.Text = header;
        }
    }
}