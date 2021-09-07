using System;
using UIKit;

namespace Chatterin.Services
{
    public static class ImageUtilityService
    {
        public static Lazy<UIImage> SystemCamera = new Lazy<UIImage>(UIImage.GetSystemImage("camera.fill"));
        public static Lazy<UIImage> SendIcon = new Lazy<UIImage>(UIImage.FromBundle("sendIcon"));
        public static Lazy<UIImage> GreenCheckMark = new Lazy<UIImage>(UIImage.FromBundle("greenCheck"));
        public static Lazy<UIImage> RedX = new Lazy<UIImage>(UIImage.FromBundle("redX"));
        public static Lazy<UIImage> ReceiverBubble = new Lazy<UIImage>(UIImage.FromBundle("receiverBubble"));
        public static Lazy<UIImage> SenderBubble = new Lazy<UIImage>(UIImage.FromBundle("senderBubble"));
    }
}
