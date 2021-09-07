using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chatterin.ClassLibrary.Mappers
{
    public static class ConversationMapper
    {
        public static ConversationDto ToDto(this Conversation entity)
        {
            var dto = new ConversationDto { CreatedUtc = entity.CreatedUtc, Id = entity.Id };

            if (entity.UserConversations != null)
            {
                dto.UserConversations = entity.UserConversations.ToDtos();
            }

            return dto;
        }

        public static IQueryable<ConversationDto> ToDtos(this IQueryable<Conversation> entities)
        {
            return entities.Select(e => e.ToDto());
        }

        public static IEnumerable<ConversationDto> ToDtos(this IEnumerable<Conversation> entities)
        {
            return entities.Select(e => e.ToDto());
        }
    }
}
