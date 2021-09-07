using Chatterin.ClassLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chatterin.Services
{
    public interface IConversationService
    {
        Task<ApiResult<IEnumerable<ConversationDto>>> GetUsersConversations(int userId);
        Task<ApiResult<ConversationDto>> AddMessageToConversation(AddMessageDto dto, int currentUserId);
    }
}
