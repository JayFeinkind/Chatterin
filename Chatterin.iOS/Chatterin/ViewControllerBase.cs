using System;
using System.Threading.Tasks;
using Chatterin.ViewModels;
using UIKit;

namespace Chatterin
{
    public abstract class ViewControllerBase : UIViewController
    {
        public ViewControllerBase(IntPtr handle) : base(handle)
        {
        }

        public override void LoadView()
        {
            base.LoadView();

            if (ViewModel != null)
            {
                ViewModel.NavigationRequested = InvokeNavigationHandler;
                ViewModel.DataIsReady = DataIsReadyHandler;
                ViewModel.ShowUIMessage = (str, cb) => InvokeOnMainThread(() => ShowMessage(str, cb));
            }
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (ViewModel != null)
            {
                Task.Run(ViewModel.StartLoadingData);
            }
        }

        private void ShowMessage(string message, Action callBack)
        {
            var controller = UIAlertController.Create(string.Empty, message, UIAlertControllerStyle.Alert);
            controller.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, (action) => callBack?.Invoke()));
            PresentViewController(controller, true, null);
        }

        private void DataIsReadyHandler()
        {
            InvokeOnMainThread(ViewModelDataLoaded);
        }

        private void InvokeNavigationHandler(IViewModelBase viewModel)
        {
            InvokeOnMainThread(() => NavigationRequestedHandler(viewModel));
        }

        protected abstract void NavigationRequestedHandler(IViewModelBase viewModel);

        protected abstract void ViewModelDataLoaded();

        public abstract IViewModelBase ViewModel { get; set; }
    }
}
