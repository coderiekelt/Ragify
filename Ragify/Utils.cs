using System;

namespace Ragify
{
    public static class Utils
    {
        public static int GetCurrentTimestamp()
        {
            long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            ticks /= 10000000;
            return Int32.Parse(ticks.ToString());
        }

        public static int GetTimeStamp(DateTime date)
        {
            long ticks = date.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            ticks /= 10000000;
            return Int32.Parse(ticks.ToString());
        }

    }
}
