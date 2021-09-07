using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chatterin.ClassLibrary;
using Chatterin.Services;

namespace Chatterin.ViewModels
{
    public class MessagesViewModel : ViewModelBase
    {
        ISettingsService _settingsService;

        public MessagesViewModel(IDependencyService dependencyService, ISettingsService settingsService) : base(dependencyService)
        {
            _settingsService = settingsService;
        }

        protected override Task LoadData()
        {
            return Task.Run(SetupList);
        }

        public void SendButtonPressed(string message)
        {
            var newMessage = new Message
            {
                MessageTxt = message,
                SentUtc = DateTime.UtcNow,
                UserConversationId = Conversation.Id
            };

            foreach (var userConversation in Conversation.UserConversations)
            {
                if (userConversation.UserId == _settingsService.UserId)
                {
                    
                }
            }
        }

        private void SetupList()
        {
            var messages = Conversation.UserConversations.SelectMany(uc => uc.Messages);

            var dtos = messages.Select(m => new MessageDto
            {
                MessageTxt = m.MessageTxt,
                SentUtc = m.SentUtc,
                SentByUser = IsMessageSentByUser(m.UserConversationId),
                SentByName = GetSentByName(m.UserConversationId)
            })
            .OrderBy(m => m.SentUtc);

            List<MessageGroupDto> groups = new List<MessageGroupDto>();

            for (int counter = 0; counter < dtos.Count(); counter++)
            {
                var message = dtos.ElementAt(counter);

                if (counter == 0)
                {
                    groups.Add(GetNewGroup(message));
                }
                else
                {
                    var previousMessage = dtos.ElementAt(counter - 1);

                    if ((previousMessage.SentUtc - message.SentUtc).TotalHours > 1)
                    {
                        groups.Add(GetNewGroup(message));
                    }
                    else
                    {
                        var existingGroup = groups.Last();
                        existingGroup.Messages.Add(message);
                    }
                }
            }

            Messages = groups;
        }

        private MessageGroupDto GetNewGroup(MessageDto dto)
        {
            var group = new MessageGroupDto { DisplayDateTime = dto.SentUtc.ToLocalTime() };
            group.Messages.Add(dto);
            return group;
        }

        private string GetSentByName(int userConversationId)
        {
            var result = string.Empty;

            var userConversation = Conversation.UserConversations
                        .FirstOrDefault(uc => uc.Id == userConversationId);

            if (userConversation != null)
            {
                var user = Users.Find(u => u.Id == userConversation.UserId);

                if (user != null)
                {
                    result = user.UserName;
                }
            }

            return result;
        }

        private bool IsMessageSentByUser(int userConversationId)
        {
            var result = true;

            var userConversation = Conversation.UserConversations
                        .FirstOrDefault(uc => uc.Id == userConversationId);

            if (userConversation != null)
            {
                result = userConversation.UserId == _settingsService.UserId;
            }

            return result;
        }

        public List<User> Users { get; set; } = new List<User>();
        public List<MessageGroupDto> Messages { get; private set; } = new List<MessageGroupDto>();
        public ConversationDto Conversation { get; set; }
    }
}
