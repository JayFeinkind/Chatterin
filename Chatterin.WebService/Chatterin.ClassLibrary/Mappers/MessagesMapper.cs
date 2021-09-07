using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chatterin.ClassLibrary.Mappers
{
    public static class MessagesMapper
    {
        public static MessageDto ToDto(this Message entity)
        {
            return new MessageDto 
            {
                MessageTxt = entity.MessageTxt,
                SentUtc = entity.SentUtc,
                UserConversationId = entity.UserConversationId,
                Id = entity.Id
            };
        }

        public static IQueryable<MessageDto> ToDtos(this IQueryable<Message> entities)
        {
            return entities.Select(e => e.ToDto());
        }

        public static IEnumerable<MessageDto> ToDtos(this IEnumerable<Message> entities)
        {
            return entities.Select(e => e.ToDto());
        }
    }
}
