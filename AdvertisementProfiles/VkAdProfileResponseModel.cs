using System.Collections;
using System.Collections.Generic;

namespace AdvertisementProfiles.VK.ResponseModels
{
    public class VkAdProfileResponseModel
    {
        public ICollection<Item> response { get; set; }
    }

    public class Item
    {
        public string id { get; set; }

        public string type { get; set; }

        public ICollection<StatisticsItem> stats { get; set; }
    }

    public class StatisticsItem
    {
        public string day { get; set; }
        public string month { get; set; }
        public int overall { get; set; }
        public double spent { get; set; }
        public int impressions { get; set; }
        public int clicks { get; set; }
        public int reach { get; set; }
        public int video_views { get; set; }
        public int video_views_half { get; set; }
        public int video_views_full { get; set; }
        public int video_clicks_site { get; set; }
        public int join_rate { get; set; }
    }
}
