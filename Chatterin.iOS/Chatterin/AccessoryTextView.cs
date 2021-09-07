using Foundation;
using System;
using UIKit;

namespace Chatterin
{
    public partial class AccessoryTextView : UITextField
    {
        public AccessoryTextView(IntPtr handle) : base(handle)
        {
        }


        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            var toolbar = new UIToolbar();
            var flex = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            var done = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done, (s, e) => ResignFirstResponder());

            toolbar.Items = new UIBarButtonItem[] { flex, done };
            toolbar.SizeToFit();
            this.InputAccessoryView = toolbar;
        }

        public void SetAttributedPlaceholder(string txt)
        {
            AttributedPlaceholder = new NSAttributedString(txt, new UIStringAttributes() { ForegroundColor = UIColor.White });
        }
    }
}