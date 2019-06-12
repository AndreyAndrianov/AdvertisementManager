using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementProfiles.VK.ResponseModels
{
    class VkGetAdsResponseModel
    {
        public List<BaseTableItem> response { get; set; }

        public class AdItem : BaseTableItem
        {
            public string campaign_id { get; set; }
            public string status { get; set; }
            public string approved { get; set; }
            public string all_limit { get; set; }
            public string cpc { get; set; }
            public string cpm { get; set; }
            public string impressions_limit { get; set; }
            public string ad_platform { get; set; }
            public string video { get; set; }
            public string disclaimer { get; set; }
        }
    }
}
