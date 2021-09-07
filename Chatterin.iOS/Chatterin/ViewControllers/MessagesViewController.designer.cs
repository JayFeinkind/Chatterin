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
    [Register ("MessagesViewController")]
    partial class MessagesViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView _messagesTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Chatterin.MessageTextView _messageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.NSLayoutConstraint MessageContainerBottom { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_messagesTableView != null) {
                _messagesTableView.Dispose ();
                _messagesTableView = null;
            }

            if (_messageView != null) {
                _messageView.Dispose ();
                _messageView = null;
            }

            if (MessageContainerBottom != null) {
                MessageContainerBottom.Dispose ();
                MessageContainerBottom = null;
            }
        }
    }
}