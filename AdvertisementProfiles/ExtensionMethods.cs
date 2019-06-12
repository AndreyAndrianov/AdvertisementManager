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
    }
}
