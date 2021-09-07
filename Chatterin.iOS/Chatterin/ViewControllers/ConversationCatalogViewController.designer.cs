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
    [Register ("ConversationCatalogViewController")]
    partial class ConversationCatalogViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView _conversationTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _headerLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_conversationTableView != null) {
                _conversationTableView.Dispose ();
                _conversationTableView = null;
            }

            if (_headerLabel != null) {
                _headerLabel.Dispose ();
                _headerLabel = null;
            }
        }
    }
}