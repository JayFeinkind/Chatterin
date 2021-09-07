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
    [Register ("CreateAccountViewController")]
    partial class CreateAccountViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView _activityIndicator { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView _availableImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _checkAvailabilityBtn { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _createAccount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Chatterin.AccessoryTextView _email { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Chatterin.AccessoryTextView _password { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        Chatterin.AccessoryTextView _userName { get; set; }

        [Action ("CheckAvailabilityPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CheckAvailabilityPressed (UIKit.UIButton sender);

        [Action ("CreateAccountPressed:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void CreateAccountPressed (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (_activityIndicator != null) {
                _activityIndicator.Dispose ();
                _activityIndicator = null;
            }

            if (_availableImage != null) {
                _availableImage.Dispose ();
                _availableImage = null;
            }

            if (_checkAvailabilityBtn != null) {
                _checkAvailabilityBtn.Dispose ();
                _checkAvailabilityBtn = null;
            }

            if (_createAccount != null) {
                _createAccount.Dispose ();
                _createAccount = null;
            }

            if (_email != null) {
                _email.Dispose ();
                _email = null;
            }

            if (_password != null) {
                _password.Dispose ();
                _password = null;
            }

            if (_userName != null) {
                _userName.Dispose ();
                _userName = null;
            }
        }
    }
}