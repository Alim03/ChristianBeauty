using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ChristianBeauty.Utilities
{
    public static class Utilites
    {
        public static string SplitString(string input, int length)
        {
            if (input.Length <= length)
                return input;

            string truncatedString = input.Substring(0, length) + "...";
            return truncatedString;
        }

        public static string PersianDateConverter(DateTime date)
        {

            var calendar = new PersianCalendar();
            var persianDate = new DateTime(calendar.GetYear(date), calendar.GetMonth(date), calendar.GetDayOfMonth(date));
            var result = persianDate.ToString("yyyy MMM ddd", CultureInfo.GetCultureInfo("fa-Ir"));
            return result;

        }

    }
}
