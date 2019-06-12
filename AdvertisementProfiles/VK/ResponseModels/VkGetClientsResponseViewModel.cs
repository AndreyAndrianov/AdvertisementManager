using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementProfiles.VK.ResponseModels
{
    public class VkGetClientsResponseViewModel
    {
        public List<ClientInfoItem> response { get; set; }

        public class ClientInfoItem : BaseTableItem
        {
            public double day_limit { get; set; }
            public double all_limit { get; set; }
        }
    }
}
