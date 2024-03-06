using System.Text.RegularExpressions;

namespace App.Utils
{
    public static class UtilsExtensions
    {
        public static int ReturnValidNumber(this string value)
        {
            string result = Regex.Replace(value, @"[^0-9]+", string.Empty);
            if (Regex.Match(result, @"^[0-9]+$").Success)
            {
                return int.Parse(result);
            }
            else
            {
                return 0;
            }
        }

        public static string ReturnIntToStringWithEmpty(this int value)
        {
            if (value > 0)
            {
                return value.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
