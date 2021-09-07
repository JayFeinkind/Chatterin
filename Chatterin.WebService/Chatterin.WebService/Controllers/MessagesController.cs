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
    public class MessagesController : AuthorizedController
    {
        IConversationService _conversationService;

        public MessagesController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet]
        [Authorize]
        [Route("conversations")]
        public async Task<ActionResult<ApiResult<IEnumerable<ConversationDto>>>> Conversations()
        {
            var currentUserId = GetUserIdFromClaims();
            var result = await _conversationService.GetUsersConversations(currentUserId);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        [Route("AddMessage")]
        public async Task<ActionResult<ApiResult<ConversationDto>>> AddMessageToConversation(AddMessageDto dto)
        {
            var currentUserId = GetUserIdFromClaims();
            var result = await _conversationService.AddMessageToConversation(dto, currentUserId);

            return Ok(result);
        }
    }
}
