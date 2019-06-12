using System.Threading.Tasks;
using AdvertisementProfiles.VK.ResponseModels;

namespace AdvertisementProfiles.VK
{
    public interface IVkHelper
    {
        Task<string> GetAccessToken(string code);

        Task<VkAdProfilesResponseModel> GetAdProfiles(string accessToken);

        Task<VkAdProfileResponseModel> GetAdProfileStatistics(string accessToken, long profileId);

        Task<VkGetStatisticsResponseModel> GetStatistics(
            DataTableName table, 
            PeriodItem period, 
            long accountId, 
            string accessToken,
            bool onlyActive);
    }
}
