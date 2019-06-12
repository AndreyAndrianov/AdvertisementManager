using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementProfiles.VK
{
    public interface IVkApiRequestHelper
    {
        Task<T1> MakeRequest<T1, T2>(string apiMethod, Dictionary<string, T2> parameters, string accessToken);
    }
}
