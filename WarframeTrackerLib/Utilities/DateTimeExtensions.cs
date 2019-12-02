using System;

namespace WarframeTrackerLib.Utilities {
    public static class DateTimeExtensions {
        public static bool IsBetween(this DateTime date, DateTime firstDate, DateTime secondDate) {
            if (firstDate == null)
                throw new ArgumentNullException("firstDate");
            if (secondDate == null)
                throw new ArgumentNullException("secondDate");

            if (firstDate == secondDate)
                throw new ArgumentException("The two dates supplied are the same.");

            if (firstDate > secondDate) {
                if (date > firstDate && date < secondDate)
                    return true;
                else
                    return false;
            } else {
                if (date > secondDate && date < secondDate)
                    return true;
                else
                    return false;
            }
        }
    }
}
