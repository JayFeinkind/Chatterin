using Chatterin.Services;
using Foundation;
using System;
using System.ComponentModel;
using UIKit;

namespace Chatterin
{
    [Register("MessageTextView"), DesignTimeVisible(true)]
    public class MessageTextView : UIView
    {
        UITextField _textField = new UITextField { TranslatesAutoresizingMaskIntoConstraints = false };
        UIButton _sendButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };
        UIButton _photoButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };

        public Action<string> SendButtonClicked { get; set; }

        public MessageTextView (IntPtr handle) : base (handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            TranslatesAutoresizingMaskIntoConstraints = false;

            AddPhotoButton();
            AddSendButton();
            AddTextField();
        }

        private void AddTextField()
        {
            AddSubview(_textField);

            _textField.Placeholder = "Message";
            _textField.Text = string.Empty;
            _textField.BorderStyle = UITextBorderStyle.RoundedRect;

            NSLayoutConstraint.Create(_photoButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _textField, NSLayoutAttribute.Left, 1, -5).Active = true;
            NSLayoutConstraint.Create(_textField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _sendButton, NSLayoutAttribute.Left, 1, -5).Active = true;
            NSLayoutConstraint.Create(_textField, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 35).Active = true;
            NSLayoutConstraint.Create(_textField, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0).Active = true;
            NSLayoutConstraint.Create(_textField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, this, NSLayoutAttribute.Top, 1, 0).Active = true;
        }

        private void AddPhotoButton()
        {
            AddSubview(_photoButton);

            _photoButton.SetImage(ImageUtilityService.SystemCamera.Value, UIControlState.Normal);

            NSLayoutConstraint.Create(_photoButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 35).Active = true;
            NSLayoutConstraint.Create(_photoButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1, 35).Active = true;

            NSLayoutConstraint.Create(_photoButton, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, this, NSLayoutAttribute.Bottom, 1, 0).Active = true;
            NSLayoutConstraint.Create(this, NSLayoutAttribute.Left, NSLayoutRelation.Equal, _photoButton, NSLayoutAttribute.Left, 1, 0).Active = true;
        }

        private void AddSendButton()
        {
            AddSubview(_sendButton);

            _sendButton.SetImage(ImageUtilityService.SendIcon.Value, UIControlState.Normal);

            _sendButton.TouchUpInside += SendButtonPressed;

            NSLayoutConstraint.Create(_sendButton, NSLayoutAttribute.Height, NSLayoutRelation.Equal, 1, 25).Active = true;
            NSLayoutConstraint.Create(_sendButton, NSLayoutAttribute.Width, NSLayoutRelation.Equal, 1, 25).Active = true;
            NSLayoutConstraint.Create(_sendButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, this, NSLayoutAttribute.CenterY, 1, 0).Active = true;
            NSLayoutConstraint.Create(this, NSLayoutAttribute.Right, NSLayoutRelation.Equal, _sendButton, NSLayoutAttribute.Right, 1, 0).Active = true;
        }

        private void SendButtonPressed(object sender, EventArgs args)
        {
            var text = _textField.Text;

            if (!string.IsNullOrWhiteSpace(text))
            {
                SendButtonClicked?.Invoke(text);
            }
        }

        public void SetEditing(bool editing)
        {
            if (editing) _textField.BecomeFirstResponder();
            else _textField.EndEditing(false);
        }
    }
}