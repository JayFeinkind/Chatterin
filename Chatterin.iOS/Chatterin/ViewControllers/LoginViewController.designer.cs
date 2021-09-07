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
    [Register ("LoginViewController")]
    partial class LoginViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView _activityIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _createAccount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Chatterin.AccessoryTextView _password { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _signIn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Chatterin.AccessoryTextView _userName { get; set; }

        [Action ("CreateAccountPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CreateAccountPressed (UIKit.UIButton sender);

        [Action ("LogginPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LogginPressed (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (_activityIndicator != null) {
                _activityIndicator.Dispose ();
                _activityIndicator = null;
            }

            if (_createAccount != null) {
                _createAccount.Dispose ();
                _createAccount = null;
            }

            if (_password != null) {
                _password.Dispose ();
                _password = null;
            }

            if (_signIn != null) {
                _signIn.Dispose ();
                _signIn = null;
            }

            if (_userName != null) {
                _userName.Dispose ();
                _userName = null;
            }
        }
    }
}