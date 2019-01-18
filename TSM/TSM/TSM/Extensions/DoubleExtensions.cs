using System;
using System.Collections.Generic;
using System.Text;

namespace TSM.Extensions
{
    public static class DoubleExtensions
    {
        public static DateTime FromTimeStampToDateTime(this double timeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
