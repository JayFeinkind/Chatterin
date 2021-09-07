using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chatterin.ClassLibrary.Mappers
{
    public static class UserConversationMapper
    {
        public static UserConversationDto ToDto(this UserConversation entity)
        {
            var dto = new UserConversationDto
            {
                ConversationId = entity.ConversationId,
                UserId = entity.UserId,
                Archived = entity.Archived,
                Id = entity.Id
            };

            if (entity.Messages != null)
            {
                dto.Messages = entity.Messages.ToDtos();
            }

            return dto;
        }

        public static IQueryable<UserConversationDto> ToDtos(this IQueryable<UserConversation> entities)
        {
            return entities.Select(e => e.ToDto());
        }

        public static IEnumerable<UserConversationDto> ToDtos(this IEnumerable<UserConversation> entities)
        {
            return entities.Select(e => e.ToDto());
        }
    }
}
