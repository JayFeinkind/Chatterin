using Chatterin.ViewModels;
using CoreGraphics;
using Foundation;
using System;
using UIKit;

namespace Chatterin
{
    public partial class MessagesViewController : ViewControllerBase
    {
        MessagesViewModel _viewModel;
        NSObject _hideNotification;
        NSObject _showNotification;
        readonly int ContainerDefaultHeight = 0;

        public MessagesViewController (IntPtr handle) : base (handle)
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
                _viewModel = value as MessagesViewModel;
            }
        }

        public override void LoadView()
        {
            base.LoadView();

            _messagesTableView.EstimatedRowHeight = 25;
            _messagesTableView.EstimatedSectionHeaderHeight = 28;
            _messagesTableView.RowHeight = UITableView.AutomaticDimension;
            _messagesTableView.SectionHeaderHeight = UITableView.AutomaticDimension;

            _messagesTableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            _messagesTableView.TableFooterView = new UIView();
            _messagesTableView.RegisterNibForCellReuse(ReceiverTableViewCell.Nib, ReceiverTableViewCell.Key);
            _messagesTableView.RegisterNibForCellReuse(SenderTableViewCell.Nib, SenderTableViewCell.Key);

            _messagesTableView.AddGestureRecognizer(new UITapGestureRecognizer(() => _messageView.SetEditing(false)));

            _messageView.SendButtonClicked = _viewModel.SendButtonPressed;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _hideNotification = UIKeyboard.Notifications.ObserveWillHide(HideCallback);
            _showNotification = UIKeyboard.Notifications.ObserveWillShow(ShowCallback);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            NSNotificationCenter.DefaultCenter.RemoveObserver(_showNotification);
            NSNotificationCenter.DefaultCenter.RemoveObserver(_hideNotification);
        }

        protected override void NavigationRequestedHandler(IViewModelBase viewModel)
        {
            
        }

        protected override void ViewModelDataLoaded()
        {
            if (_messagesTableView.Source == null)
            {
                _messagesTableView.Source = new MessagesTableViewSource(_viewModel, () => _messageView.SetEditing(false));
            }

            _messagesTableView.ReloadData();
        }

        #region TextField animations

        private void AnimateWithKeyboard(UIKeyboardEventArgs args, Action<CGRect> animation)
        {
            var frameValue = args.FrameEnd;
            var duration = args.AnimationDuration;
            var curve = args.AnimationCurve;

            var animator = new UIViewPropertyAnimator(duration, curve, () => {
                animation?.Invoke(frameValue);
                View.LayoutIfNeeded();
            });

            animator.StartAnimation();
        }

        private void ShowCallback(object sender, UIKeyboardEventArgs args)
        {
            AnimateWithKeyboard(args, (rect) => {
                MessageContainerBottom.Constant = ContainerDefaultHeight + rect.Height;
            });
        }

        private void HideCallback(object sender, UIKeyboardEventArgs args)
        {
            AnimateWithKeyboard(args, (rect) => {
                MessageContainerBottom.Constant = ContainerDefaultHeight;
            });
        }

        #endregion

        private class MessagesTableViewSource : UITableViewSource
        {
            MessagesViewModel _viewModel;
            Action _rowSelected;

            public MessagesTableViewSource(MessagesViewModel viewModel, Action rowSelected)
            {
                _rowSelected = rowSelected;
                _viewModel = viewModel;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var message = _viewModel.Messages[indexPath.Section].Messages[indexPath.Row];

                var cell = message.SentByUser ?
                    tableView.DequeueReusableCell(SenderTableViewCell.Key) :
                    tableView.DequeueReusableCell(ReceiverTableViewCell.Key);
        

                ((IMessageTableViewCell)cell).UpdateCell(message);

                cell.SelectionStyle = UITableViewCellSelectionStyle.None;

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return _viewModel.Messages[(int)section].Messages.Count;
            }

            public override nint NumberOfSections(UITableView tableView)
            {
                return _viewModel.Messages.Count;
            }

            public override UIView GetViewForHeader(UITableView tableView, nint section)
            {
                var view = MessagesHeaderView.Create();

                var header = _viewModel.Messages[(int)section].GetFormatedDateStr();

                view.UpdateView(header);

                return view;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                _rowSelected?.Invoke();
            }
        }
    }
}