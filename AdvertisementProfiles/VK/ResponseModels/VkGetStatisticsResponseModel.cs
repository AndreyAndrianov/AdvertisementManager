using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementProfiles.VK.ResponseModels
{
    public class VkGetStatisticsResponseModel
    {
        public List<StatisticsItem> response { get; set; }

        public class StatisticsItem
        {
            public string id { get; set; }
            public string type { get; set; }
            public List<StatsItem> stats { get; set; }
            public string name { get; set; }
        }

        public class StatsItem 
        {
            public string day { get; set; }
            public string month { get; set; }
            public string overall { get; set; }
            public double? spent { get; set; }
            public int? impressions { get; set; }
            public int? clicks { get; set; }
            public int? reach { get; set; }
            public int? video_views { get; set; }
            public int? video_views_half { get; set; }
            public int? video_views_full { get; set; }
            public int? video_clicks_site { get; set; }
            public int? join_rate { get; set; }
        }
    }
}
