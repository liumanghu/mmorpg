using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    public class TimeUtil
    {
        public static double timestamp
        {
            get { return GetTimestamp(DateTime.Now); }
        }

        private static DateTime GetTime(long timestamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long ITime = timestamp * 10000000;
            TimeSpan toNow = new TimeSpan(ITime);
            return dateTimeStart.Add(toNow);
        }

        public static double GetTimestamp(DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (time - startTime).TotalSeconds;
        }
    }
}
