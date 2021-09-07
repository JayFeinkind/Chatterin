// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Chatterin
{
    [Register ("ConversationsTableViewCell")]
    partial class ConversationsTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView _contactImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _detail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _header { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _timeStamp { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_contactImage != null) {
                _contactImage.Dispose ();
                _contactImage = null;
            }

            if (_detail != null) {
                _detail.Dispose ();
                _detail = null;
            }

            if (_header != null) {
                _header.Dispose ();
                _header = null;
            }

            if (_timeStamp != null) {
                _timeStamp.Dispose ();
                _timeStamp = null;
            }
        }
    }
}