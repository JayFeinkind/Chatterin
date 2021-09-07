using System;
using System.Collections.Generic;
using System.Linq;

namespace Chatterin.ClassLibrary
{
    public class ConversationDto
    {
        public int Id { get; set; }
        public DateTime CreatedUtc { get; set; }

        public string GetLastMessageText()
        {
            var result = string.Empty;
            var lastMessage = UserConversations?.SelectMany(uc => uc.Messages)?.OrderByDescending(uc => uc.SentUtc)?.FirstOrDefault();

            if (lastMessage != null)
            {
                result = lastMessage.MessageTxt;
            }

            return result;
        }

        public string GetLastMessageSent()
        {
            var result = string.Empty;

            var lastDate = UserConversations?.SelectMany(uc => uc.Messages)?.OrderByDescending(uc => uc.SentUtc)?.FirstOrDefault()?.SentUtc;

            if (lastDate != null)
            {
                if (lastDate.Value.Date == DateTime.UtcNow.Date)
                {
                    if ((lastDate.Value - DateTime.UtcNow).TotalMinutes <= 1)
                    {
                        result = "Now";
                    }
                    else
                    {
                        result = lastDate.Value.ToLocalTime().ToShortTimeString();
                    }
                }
                else if ((lastDate.Value.Date - DateTime.UtcNow.Date).TotalDays == 1)
                {
                    result = "Yesterday";
                }
                else if (Math.Abs((lastDate.Value.Date - DateTime.UtcNow.Date).TotalDays) < 7)
                {
                    result = lastDate.Value.ToLocalTime().DayOfWeek.ToString();
                }
                else
                {
                    result = lastDate.Value.ToLocalTime().ToShortDateString();
                }
            }

            return result;
        }

        public string GetUserNameStr(int currentUserId)
        {
            var result = string.Empty;
            var others = UserConversations?.Where(uc => uc.UserId != currentUserId);

            if (others?.Any() == true)
            {
                var firstUser = others.FirstOrDefault().UserName;

                switch (others.Count())
                {
                    case 1:
                        result = firstUser;
                        break;
                    case 2:
                        result = firstUser + " & " + others.LastOrDefault().UserName;
                        break;
                    default:
                        result = firstUser + ", " + others.ElementAt(1).UserName + ", +" + (others.Count() - 2);
                        break;
                }
            }

            return result;
        }

        public IEnumerable<UserConversationDto> UserConversations { get; set; }
    }
}
