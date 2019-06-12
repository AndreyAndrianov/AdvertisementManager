using System.Collections.Generic;
using System.Linq;

namespace AdvertisementProfiles
{
    public static class ExtensionMethods
    {
        public static string GenerateGetParameters<T>(this Dictionary<string, T> parameters)
        {
            return string.Join("&", parameters.Keys.Select(k => $"{k}={parameters[k].ToString()}"));
        }

        public static string FormatNumber(this int number, int signNumber)
        {
            var stringNumber = number.ToString();
            if (stringNumber.Length >= signNumber)
            {
                return stringNumber;
            }

            return string.Join("", Enumerable.Range(0, signNumber - stringNumber.Length).Select(_ => "0")) +
                   stringNumber;
        }
    }
}
