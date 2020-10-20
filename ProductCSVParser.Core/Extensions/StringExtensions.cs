using System;
using System.Globalization;

namespace ProductCSVParser.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse a string to a nullable datetime object
        /// </summary>
        public static DateTime? ToDateTime(this string dateTime, DateTimeFormatInfo dateTimeFormatInfo = null, DateTimeStyles dateTimeStyles = DateTimeStyles.None)
        {
            return DateTime.TryParse(dateTime, dateTimeFormatInfo ?? DateTimeFormatInfo.CurrentInfo, dateTimeStyles, out DateTime result)
                ? result
                : default(DateTime?);
        }
    }
}