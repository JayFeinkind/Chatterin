using Foundation;
using System;
using UIKit;

namespace Chatterin
{
    public partial class MainTabBarViewController : UITabBarController
    {
        public MainTabBarViewController (IntPtr handle) : base (handle)
        {
        }

        public override void LoadView()
        {
            base.LoadView();

            NavigationController.NavigationBar.BarTintColor = UIColor.Orange;
            NavigationController.NavigationBar.TintColor = UIColor.Black;
        }
    }
}