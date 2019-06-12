using System.Collections.Generic;

namespace AdvertisementProfiles.VK.ResponseModels
{
    public class VkAdProfilesResponseModel
    {
        public IEnumerable<AdProfile> response { get; set; }

        public class AdProfile
        {
            public string account_id { get; set; }
            public string account_type { get; set; }
            public int account_status { get; set; }
            public string access_role { get; set; }
        }
    }
}
