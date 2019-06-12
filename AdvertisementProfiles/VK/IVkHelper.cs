using System.Threading.Tasks;
using AdvertisementProfiles.VK.ResponseModels;

namespace AdvertisementProfiles.VK
{
    public interface IVkHelper
    {
        Task<string> GetAccessToken(string code);

        Task<VkAdProfilesResponseModel> GetAdProfiles(string accessToken);

        Task<VkAdProfileResponseModel> GetAdProfileStatistics(string accessToken, long profileId);
    }
}
