using System.Collections.Generic;

namespace AdverticementManager.ViewModels
{
    public class VkAdProfilesViewModel
    {
        public ICollection<AdProfile> AdProfiles { get; set; }

        public class AdProfile
        {
            public string AccountId { get; set; }

            public string AccountType { get; set; }

            public int AccountStatus { get; set; }

            public string AccessRole { get; set; }
        }
    }
}
