using Foundation;
using System;
using UIKit;

namespace Chatterin
{
    public partial class LoginTextViewContainer : UIView
    {
        public LoginTextViewContainer (IntPtr handle) : base (handle)
        {
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();

            this.BackgroundColor = UIColor.White.ColorWithAlpha(0.5f);
        }
    }
}