using System;
using UIKit;

namespace Chatterin
{
    public static class IosAppUtilities
    {
        public static void SetNavigationBarHidden(this UINavigationController navigationController, bool hidden)
        {
            if (navigationController != null)
            {
                navigationController.SetNavigationBarHidden(hidden, true);
            }
        }

    }
}
