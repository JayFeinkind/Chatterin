using Chatterin.ClassLibrary;
using Chatterin.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Chatterin.ClassLibrary.Mappers;

namespace Chatterin.Services
{
    public class ConversationService : ServiceBase<Conversation>, IConversationService
    {
        public ConversationService(ChatterinContext context) : base(context) { }

        public async Task<ApiResult<IEnumerable<ConversationDto>>> GetUsersConversations(int userId)
        {
            if (userId == 0) throw new ArgumentException("Invalid user Id");

            return new ApiResult<IEnumerable<ConversationDto>>
            {
                Result = await ReadOnlyQuery
                .Include(c => c.UserConversations).ThenInclude(uc => uc.User)
                .Include(c => c.UserConversations).ThenInclude(uc => uc.Messages)
                .Where(c => c.UserConversations.Any(uc => uc.UserId == userId))
                .ToDtos()
                .ToListAsync()
            };
        }

        public async Task<ApiResult<ConversationDto>> AddMessageToConversation(AddMessageDto dto, int currentUserId)
        {
            if (dto.UserConversationId == 0) throw new ArgumentException("Invalid user Id");
            if (string.IsNullOrWhiteSpace(dto.Text)) throw new ArgumentException("Missing message");

            var result = new ApiResult<ConversationDto>();

            var userConversation = await Context
                .UserConversations
                .FirstOrDefaultAsync(uc => uc.Id == dto.UserConversationId);

            if (userConversation != null)
            {
                if (userConversation.UserId != currentUserId) throw new InvalidOperationException("Cannot add message to other user's conversation");

                userConversation.Messages.Add(new Message 
                {
                    MessageTxt = dto.Text,
                    SentUtc = dto.SentUtc
                });

                await Context.SaveChangesAsync();

                var conversation = await ReadOnlyQuery
                    .Include(c => c.UserConversations).ThenInclude(uc => uc.User)
                    .Include(c => c.UserConversations).ThenInclude(uc => uc.Messages)
                    .FirstOrDefaultAsync(c => c.UserConversations.Any(uc => uc.Id == dto.UserConversationId));

                result.Result = conversation.ToDto();
            }

            return result;
        }
    }
}
