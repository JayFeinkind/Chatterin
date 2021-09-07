using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using Chatterin.Services;

namespace Chatterin.ViewModels
{
    public class ConversationCatalogViewModel : ViewModelBase
    {
        public readonly string EmptySetMsg = "You don't have any active conversations. Time to start some!";
        public readonly string FetchingDataMsg = "Retrieving conversations...";
        public readonly string PageTitle = "Conversations";
        IUserService _userService;
        IConversationService _conversationService;
        List<ConversationDto> _conversations = new List<ConversationDto>();
        ISettingsService _settingsService;
        List<User> _users = new List<User>();

        public ConversationCatalogViewModel(
            IDependencyService dependencyService,
            IUserService userService,
            IConversationService conversationService,
            ISettingsService settingsService) : base(dependencyService)
        {
            _settingsService = settingsService;
            _conversationService = conversationService;
            _userService = userService;
        }

        protected override async Task LoadData()
        {
            _users = await _userService.GetAndInsert();
            var conversations = await _conversationService.GetAndInsert();

            _conversations = conversations.Select(c => new ConversationDto
            {
                Id = c.Id,
                CreatedUtc = c.CreatedUtc,
                UserConversations = c.UserConversations.Select(uc => new UserConversationDto
                {
                    Id = uc.Id,
                    Archived = uc.Archived,
                    ConversationId = uc.ConversationId,
                    Messages = uc.Messages,
                    UserId = uc.UserId,
                    UserName = _users.Find(u => u.Id == uc.UserId)?.UserName
                })
            }).ToList();
        }

        public void NavigateToConversation(ConversationDto dto)
        {
            var viewModel = DependencyService.Resolve<MessagesViewModel>();
            viewModel.Conversation = dto;
            viewModel.Users = _users;
            NavigationRequested?.Invoke(viewModel);
        }

        public List<ConversationDto> Conversations => _conversations;
        public int CurrentUserId => _settingsService.UserId;
    }
}
