// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Chatterin
{
    [Register ("MessagesHeaderView")]
    partial class MessagesHeaderView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _header { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_header != null) {
                _header.Dispose ();
                _header = null;
            }
        }
    }
}