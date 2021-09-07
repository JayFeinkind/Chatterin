using Chatterin.ViewModels;
using Foundation;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Chatterin
{
    public partial class LoginViewController : ViewControllerBase
    {
        private LoginViewModel _viewModel;

        public override IViewModelBase ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value as LoginViewModel;
            }
        }

        public LoginViewController (IntPtr handle) : base (handle)
        {
            ViewModel = AppDelegate.DependencyService.Resolve<LoginViewModel>();
        }

        public override void LoadView()
        {
            base.LoadView();

            _userName.SetAttributedPlaceholder("Username");
            _password.SetAttributedPlaceholder("Password");
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            NavigationController.SetNavigationBarHidden(true);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            NavigationController.SetNavigationBarHidden(false);
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            _signIn.ClipsToBounds = true;

            _signIn.AddShadow(20f);
        }

        protected override void ViewModelDataLoaded()
        {
            // nothing to load
        }

        partial void CreateAccountPressed(UIButton sender)
        {
            _viewModel.NavigateToCreateAccount();
        }

        private void ChangeSubmitUI(bool busy)
        {
            _signIn.Enabled = !busy;
            _userName.Enabled = !busy;
            _password.Enabled = !busy;

            if (busy) _activityIndicator.StartAnimating();
            else _activityIndicator.StopAnimating();
        }

        partial void LogginPressed(UIButton sender)
        {
            var username = _userName.Text;
            var password = _password.Text;
            ChangeSubmitUI(true);
            Task.Run(() => Login(username, password));
        }

        private async Task Login(string username, string password)
        {
            if (!await _viewModel.Login(username, password))
            {
                InvokeOnMainThread(() => ChangeSubmitUI(false));
            }
        }

        protected override void NavigationRequestedHandler(IViewModelBase viewModel)
        {
            if (viewModel is CreateAccountViewModel)
            {
                var controller = UIStoryboard.FromName("Main", null).InstantiateViewController(nameof(CreateAccountViewController)) as ViewControllerBase;
                controller.ViewModel = viewModel;
                NavigationController.PushViewController(controller, true);
            }
            else if (viewModel is ConversationCatalogViewModel)
            {
                var navController = UIStoryboard.FromName("Messages", null).InstantiateInitialViewController();
                navController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;

                NavigationController.DismissViewController(true, () => PresentViewController(navController, true, null));
            }
        }
    }
}


