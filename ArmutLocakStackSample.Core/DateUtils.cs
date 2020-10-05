using System;

namespace ArmutLocalStackSample.Core
{
    public static class DateUtils
    {
        public static string ToComparableDateString(this DateTime o) => o.ToString("s");
    }
}