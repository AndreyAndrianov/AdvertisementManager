using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IVkApiRequestHelper _requestHelper;

        public VkHelper(IConfiguration configuration, IVkApiRequestHelper requestHelper)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _requestHelper = requestHelper;
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

        public async Task<VkGetStatisticsResponseModel> GetStatistics(
            DataTableName table, 
            PeriodItem period, 
            long accountId, 
            string accessToken,
            bool onlyActive)
        {
            switch (table)
            {
                case DataTableName.Client:
                    var clients = await GetAllClients(accountId, accessToken);
                    break;
                case DataTableName.Ad:
                    var ads = await GetAllAds(accountId, accessToken, onlyActive);
                    var statistics = await GetStatics(table, period, accountId, accessToken, ads.response);
                    statistics.response.ForEach(s => s.name = ads.response.FirstOrDefault(a => a.id == s.id)?.name);
                    return statistics;
                case DataTableName.Campaign:
                    var comps = await GetAllCampaigns(accountId, accessToken, onlyActive);
                    break;
            }

            return null;
        }

        private async Task<VkGetStatisticsResponseModel> GetStatics(DataTableName table, PeriodItem period, long accountId, string accessToken, List<BaseTableItem> items)
        {
            var parameters = new Dictionary<string, string>
            {
                {"account_id", accountId.ToString()},
                {"ids_type", table.ToString().ToLower() },
                {"ids", string.Join(",", items.Select(i => i.id)) },
                {"period", period.ToString().ToLower() },
                {"date_from", GenerateDateFrom(period) },
                {"date_to", GenerateDateFrom(period, true) },
            };

            return await _requestHelper.MakeRequest<VkGetStatisticsResponseModel, string>(
                "ads.getStatistics", 
                parameters, 
                accessToken);
        }

        private string GenerateDateFrom(PeriodItem period, bool now = false)
        {
            DateTime begin;
            switch (period)
            {
                case PeriodItem.Overall:
                    return "0";
                case PeriodItem.Day:
                    begin = now ? DateTime.Now : DateTime.Now.AddDays(-1);
                    return $"{begin.Year}-{begin.Month.FormatNumber(2)}-{begin.Day.FormatNumber(2)}";
                case PeriodItem.Month:
                    begin = now ? DateTime.Now : DateTime.Now.AddMonths(-1);
                    return $"{begin.Year}-{begin.Month.FormatNumber(2)}";
                default:
                    throw new InvalidOperationException("Период не поддерживается");
            }
        }

        private async Task<VkGetAdsResponseModel> GetAllAds(long accountId, string accessToken, bool onlyActive)
        {
            var parameters = new Dictionary<string, string>
            {
                {"account_id", accountId.ToString()},
                {"campaign_ids", "null" },
                {"ad_ids", "null" },
                {"include_deleted", onlyActive ? "0" : "1" }
            };

            return await _requestHelper.MakeRequest<VkGetAdsResponseModel, string>(
                "ads.getAds",
                parameters,
                accessToken);
        }

        private async Task<VkGetCampaignsResponseModel> GetAllCampaigns(long accountId, string accessToken, bool onlyActive)
        {
            var parameters = new Dictionary<string, string>
            {
                {"account_id", accountId.ToString()},
                {"campaign_ids", "null" },
                {"include_deleted", onlyActive ? "0" : "1" }
            };

            return await _requestHelper.MakeRequest<VkGetCampaignsResponseModel, string>(
                "ads.getCampaigns",
                parameters,
                accessToken);
        }

        private async Task<VkGetClientsResponseViewModel> GetAllClients(long accountId, string accessToken)
        {
            var parameters = new Dictionary<string, long>
            {
                {"account_id", accountId}
            };
            return await _requestHelper.MakeRequest<VkGetClientsResponseViewModel, long>(
                "ads.getClients", 
                parameters,
                accessToken);
        }
    }
}
