using System;

namespace AudenTest.Extensions
{
    internal static class Extensions
    {
        public static DateTime GetNextDayDate(this DateTime from, DayOfWeek dayOfWeek)
        {
            var start = (int)from.DayOfWeek;
            var target = (int)dayOfWeek;
            if (target <= start)
                target += 7;

            return from.AddDays(target - start);
        }

    }
}
