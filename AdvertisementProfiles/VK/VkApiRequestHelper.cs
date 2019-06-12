using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AdvertisementProfiles.VK
{
    public class VkApiRequestHelper : IVkApiRequestHelper
    {
        private const string BaseUri = "https://api.vk.com/method/{0}?";

        public async Task<T1> MakeRequest<T1, T2>(string apiMethod, Dictionary<string, T2> parameters, string accessToken)
        {
            var requestUri = GenerateRequestUri(apiMethod, parameters, accessToken);
            var client = new HttpClient();
            var result = await client.GetAsync(requestUri);
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T1>(content);
        }

        private string GenerateRequestUri<T>(string apiMethod, Dictionary<string, T> parameters, string accessToken)
        {
            var baseUri = string.Format(BaseUri, apiMethod);
            var preparedParameters = AddMandatoryParametersToRequest(parameters, accessToken);
            var requestUri = baseUri + preparedParameters.GenerateGetParameters();

            return requestUri;
        }

        private Dictionary<string, string> AddMandatoryParametersToRequest<T>(Dictionary<string, T> parameters, string accessToken)
        {
            var resultParameters = new Dictionary<string, string>();
            foreach (var kvp in parameters)
            {
                resultParameters.Add(kvp.Key, kvp.Value.ToString());
            }

            if (!resultParameters.ContainsKey("v") && !resultParameters.ContainsKey("V"))
            {
                resultParameters.Add("v", "5.95"); 
            }

            if (!resultParameters.ContainsKey("access_token"))
            {
                resultParameters.Add("access_token", accessToken);
            }

            return resultParameters;
        }
    }
}
