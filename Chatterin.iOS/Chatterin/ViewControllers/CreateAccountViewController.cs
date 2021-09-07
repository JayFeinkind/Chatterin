using Chatterin.Services;
using Chatterin.ViewModels;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Chatterin
{
    public partial class CreateAccountViewController : ViewControllerBase
    {
        CreateAccountViewModel _viewModel;

        public CreateAccountViewController (IntPtr handle) : base (handle)
        {
        }

        public override IViewModelBase ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value as CreateAccountViewModel;
            }
        }

        public override void LoadView()
        {
            base.LoadView();

            _userName.SetAttributedPlaceholder("Username");
            _email.SetAttributedPlaceholder("Email Address");
            _password.SetAttributedPlaceholder("Password");

            _availableImage.Hidden = true;
            _activityIndicator.Hidden = true;
            _activityIndicator.HidesWhenStopped = true;

            _createAccount.AddShadow(20f);
            _checkAvailabilityBtn.AddShadow(20f);
        }

        private void AccountCreatedSuccessfully()
        {
            InvokeOnMainThread(() => NavigationController?.PopViewController(true));
        }

        protected override void ViewModelDataLoaded()
        {
            
        }

        private void StartActivityIndicator()
        {
            _activityIndicator.Hidden = false;
            _activityIndicator.StartAnimating();

            _checkAvailabilityBtn.Enabled = false;
            _createAccount.Enabled = false;
        }

        private void StopActivityIndicator()
        {
            _activityIndicator.StopAnimating();
            _checkAvailabilityBtn.Enabled = true;
            _createAccount.Enabled = true;
        }

        #region Username available

        partial void CheckAvailabilityPressed(UIButton sender)
        {
            StartActivityIndicator();

            var username = _userName.Text;
            Task.Run(() => CheckUsernameAvilability(username));
        }

        private async Task CheckUsernameAvilability(string username)
        {
            var result = await _viewModel.UserNameAvailable(username);

            InvokeOnMainThread(() => SetAvailabilityImage(result));
        }

        private void SetAvailabilityImage(bool available)
        {
            StopActivityIndicator();

            if (available)
            {
                _availableImage.Image = ImageUtilityService.GreenCheckMark.Value;
            }
            else
            {
                _availableImage.Image = ImageUtilityService.RedX.Value;
            }

            if (_availableImage.Hidden == true)
            {
                _availableImage.Hidden = false;
            }
        }

        #endregion

        partial void CreateAccountPressed(UIButton sender)
        {
            var username = _userName.Text;
            var email = _email.Text;
            var password = _password.Text;

            StartActivityIndicator();
            Task.Run(() => CreateAccount(username, email, password));
        }

        private async Task CreateAccount(string username, string email, string password)
        {
            await _viewModel.CreateAccount(username, email, password, AccountCreatedSuccessfully);

            InvokeOnMainThread(StopActivityIndicator);
        }

        protected override void NavigationRequestedHandler(IViewModelBase viewModel)
        {
            
        }
    }
}