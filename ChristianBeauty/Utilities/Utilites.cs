using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

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

            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(date);
            int month = persianCalendar.GetMonth(date);
            int day = persianCalendar.GetDayOfMonth(date);
            return $"{year}/{month.ToString("D2")}/{day.ToString("D2")}";

        }

        public static bool IsValidMobileNumber(this string input)
        {
            const string pattern = @"^09[0|1|2|3][0-9]{8}$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(input);
        }
    }
}
