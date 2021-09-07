using System;
using UIKit;

namespace Chatterin
{
    public static class UIViewExtensions
    {
        public static void AddShadow(this UIView view, nfloat cornerRadius)
        {
            UIBezierPath path = UIBezierPath.FromRoundedRect(view.Bounds, cornerRadius);
            view.Layer.MasksToBounds = false;
            view.Layer.ShadowColor = UIColor.Gray.CGColor;
            view.Layer.ShadowOffset = new CoreGraphics.CGSize(0.5f, 0.5f);
            view.Layer.ShadowOpacity = 1f;
            view.Layer.ShadowPath = path.CGPath;
        }
    }
}
