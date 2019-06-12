using System.Collections.Generic;
using AdvertisementProfiles.VK.ResponseModels;

namespace AdverticementManager.ViewModels
{
    public class VkStatisticsViewModel
    {
        public List<StatisticsItem> Statistics { get; set; }

        public VkStatisticsViewModel()
        {
        }

        public VkStatisticsViewModel(VkGetStatisticsResponseModel model)
        {
            Statistics = new List<StatisticsItem>();
            foreach (var statItem in model.response)
            {
                if (statItem.stats == null)
                    continue;
                foreach (var st in statItem.stats)
                {
                    Statistics.Add(new StatisticsItem
                    {
                        Name = statItem.name,
                        Id = statItem.id,
                        Clicks = st.clicks,
                        Impressions = st.impressions,
                        JoinRate = st.join_rate,
                        Reach = st.reach,
                        Spent = st.spent,
                        VideoClicksSite = st.video_clicks_site,
                        VideoViews = st.video_views,
                        VideoViewsFull = st.video_views_full,
                        VideoViewsHalf = st.video_views_half
                    });
                }
            }
        }
    }

    public class StatisticsItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double? Spent { get; set; }
        public int? Impressions { get; set; }
        public int? Clicks { get; set; }
        public int? Reach { get; set; }
        public int? VideoViews { get; set; }
        public int? VideoViewsHalf { get; set; }
        public int? VideoViewsFull { get; set; }
        public int? VideoClicksSite { get; set; }
        public int? JoinRate { get; set; }
    }
}
