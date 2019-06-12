using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementProfiles.VK.ResponseModels
{
    public class VkGetCampaignsResponseModel
    {
        public List<BaseTableItem> response { get; set; }

        public class CampaignItem : BaseTableItem
        {
            public string status { get; set; }
            public string day_limit { get; set; }
            public string all_limit { get; set; }
            public string start_time { get; set; }
            public string stop_time { get; set; }
        }
    }
}
