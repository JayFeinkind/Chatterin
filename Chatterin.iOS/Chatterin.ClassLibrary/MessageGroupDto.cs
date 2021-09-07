using System;
using System.Collections.Generic;
using System.Globalization;

namespace Chatterin.ClassLibrary
{
    public class MessageGroupDto
    {
        public DateTime DisplayDateTime { get; set; }
        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();

        public string GetFormatedDateStr()
        {
            var result = string.Empty;

            if (DisplayDateTime.Date == DateTime.Now.Date)
            {
                result = DisplayDateTime.ToShortTimeString();
            }
            else if ((DateTime.Now.Date - DisplayDateTime.Date).TotalDays == 1)
            {
                result = "Yesterday " + DisplayDateTime.ToShortTimeString();
            }
            else if ((DateTime.Now.Date - DisplayDateTime.Date).TotalDays < 7)
            {
                result = DisplayDateTime.Date.DayOfWeek + " " + DisplayDateTime.ToShortTimeString();
            }
            else
            {
                var formatInfo = new DateTimeFormatInfo();

                result = string.Format("{0} {1} {2}, {3}",
                    DisplayDateTime.ToString("ddd"),
                    formatInfo.GetAbbreviatedMonthName(DisplayDateTime.Month),
                    DisplayDateTime.Day,
                    DisplayDateTime.ToShortTimeString());
            }

            return result;
        }
    }
}
