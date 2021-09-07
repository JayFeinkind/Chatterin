using Chatterin.ClassLibrary;
using Chatterin.ViewModels;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Chatterin
{
    public partial class ConversationCatalogViewController : ViewControllerBase
    {
        ConversationCatalogViewModel _viewModel;

        public ConversationCatalogViewController (IntPtr handle) : base (handle)
        {
            _viewModel = AppDelegate.DependencyService.Resolve<ConversationCatalogViewModel>();
        }

        public override IViewModelBase ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value as ConversationCatalogViewModel;
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            addButton.Clicked += AddPressed;

            if (TabBarController.NavigationItem != null)
            {
                TabBarController.NavigationItem.RightBarButtonItem = addButton;
                TabBarController.NavigationItem.Title = _viewModel.PageTitle;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if (NavigationItem != null)
            {
                NavigationItem.SetRightBarButtonItem(null, true);
                TabBarController.NavigationItem.Title = string.Empty;
            }
        }

        public override void LoadView()
        {
            base.LoadView();

            _headerLabel.Text = _viewModel.FetchingDataMsg;
            _conversationTableView.Hidden = true;
            _conversationTableView.TableFooterView = new UIView();
            _conversationTableView.RegisterNibForCellReuse(ConversationsTableViewCell.Nib, ConversationsTableViewCell.Key);
            _conversationTableView.EstimatedRowHeight = 44;
            _conversationTableView.RowHeight = UITableView.AutomaticDimension;
            _conversationTableView.Source = new ConversationTableViewSource(_viewModel);
        }

        protected override void NavigationRequestedHandler(IViewModelBase viewModel)
        {
            if (viewModel is MessagesViewModel)
            {
                var controller = Storyboard.InstantiateViewController(nameof(MessagesViewController)) as MessagesViewController;
                controller.ViewModel = viewModel;

                TabBarController.NavigationController.PushViewController(controller, true);
            }
        }

        private void AddPressed(object sender, EventArgs args)
        {

        }

        protected override void ViewModelDataLoaded()
        {
            SetVisibility();

            _conversationTableView.ReloadData();
        }

        private void SetVisibility()
        {
            var emptySet = _viewModel.Conversations.Count == 0;

            _headerLabel.Text = emptySet ? _viewModel.EmptySetMsg : string.Empty;
            _conversationTableView.Hidden = emptySet;
        }

        private class ConversationTableViewSource : UITableViewSource
        {
            ConversationCatalogViewModel _viewModel;

            public ConversationTableViewSource(ConversationCatalogViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(ConversationsTableViewCell.Key)
                    as ConversationsTableViewCell;

                var conversation = _viewModel.Conversations[indexPath.Row];

                cell.UpdateCell(conversation, _viewModel.CurrentUserId);

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return _viewModel.Conversations?.Count ?? 0;
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                tableView.DeselectRow(indexPath, true);
                _viewModel.NavigateToConversation(_viewModel.Conversations[indexPath.Row]);
            }
        }
    }
}