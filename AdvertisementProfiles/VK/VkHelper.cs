using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AdverticementManager.Utils;
using AdvertisementProfiles.VK.ResponseModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdvertisementProfiles.VK
{
    public class VkHelper : IVkHelper
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public VkHelper(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<string> GetAccessToken(string code)
        { 
            var tokenUrl = string.Format(_configuration.GetValue<string>("AccessTokenUrl"), code);
            var res = await _httpClient.GetAsync(tokenUrl);
            var content = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<AccessTokenResponseModel>(content);
            return response.access_token;
        }

        public async Task<VkAdProfilesResponseModel> GetAdProfiles(string accessToken)
        {
            // TODO Обработать протухший токен
            var adsUrl = $"https://api.vk.com/method/ads.getAccounts?access_token={accessToken}&v=5.95";
            var res = await _httpClient.GetAsync(adsUrl);
            var content = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VkAdProfilesResponseModel>(content);
            //return JsonConvert.DeserializeObject<VkAdProfilesResponseModel>(content);
        }

        public async Task<VkAdProfileResponseModel> GetAdProfileStatistics(string accessToken, long profileId)
        {
            var adsUrl = "https://api.vk.com/method/ads.getStatistics?";

            var parameters = new Dictionary<string, string>();
            parameters.Add("access_token", accessToken);
            parameters.Add("v", "5.95");
            parameters.Add("account_id", profileId.ToString());
            parameters.Add("ids_type", "office");
            parameters.Add("ids", profileId.ToString());
            parameters.Add("period", "overall");
            parameters.Add("date_from", "0");
            parameters.Add("date_to", "0");

            var request = adsUrl + parameters.GenerateGetParameters<string>();

            var res = await _httpClient.GetAsync(request);
            var content = await res.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<VkAdProfileResponseModel>(content);
            return response;
        }


    }
}
