using Chatterin.ClassLibrary;
using Chatterin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatterin.WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController(IConversationService conversationService) : AuthorizedController
    {
        [HttpGet]
        [Authorize]
        [Route("conversations")]
        public async Task<ActionResult<ApiResult<IEnumerable<ConversationDto>>>> Conversations()
        {
            var currentUserId = GetUserIdFromClaims();
            var result = await conversationService.GetUsersConversations(currentUserId);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("AddMessage")]
        public async Task<ActionResult<ApiResult<ConversationDto>>> AddMessageToConversation(AddMessageDto dto)
        {
            var currentUserId = GetUserIdFromClaims();
            var result = await conversationService.AddMessageToConversation(dto, currentUserId);

            return Ok(result);
        }
    }
}
