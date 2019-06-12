using System.Threading.Tasks;
using AdverticementManager.ViewModels;
using AdvertisementProfiles.VK;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdverticementManager.Controllers
{
    [Authorize]
    public class VkAdController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IVkHelper _vkHelper;
        private readonly IMapper _mapper;

        public VkAdController(IConfiguration configuration, IVkHelper vkHelper, IMapper mapper)
        {
            _configuration = configuration;
            _vkHelper = vkHelper;
            _mapper = mapper;
        }

        [HttpGet("/VkAd")]
        public async Task<IActionResult> VkAd([FromQuery]string code)
        {
            var accessToken = Request.HttpContext.Session.GetString("vkAccessToken");
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    return Redirect(
                        "https://oauth.vk.com/authorize?client_id=7012170&display=page&redirect_uri=https://localhost:44326/VkAd&scope=98304&response_type=code&v=5.95");
                }

                accessToken = await _vkHelper.GetAccessToken(code);
                Request.HttpContext.Session.SetString("vkAccessToken", accessToken);
            }

            var profiles = await _vkHelper.GetAdProfiles(accessToken);
            var vm = _mapper.Map<VkAdProfilesViewModel>(profiles);

            return View(vm);
        }

        public async Task<IActionResult> ProfileInfo(long profileId)
        {
            // TODO обработать ситуацию, когда нет токена
            var token = Request.HttpContext.Session.GetString("vkAccessToken");
            var statistics = await _vkHelper.GetAdProfileStatistics(token, profileId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetData(VkAdProfileDisplaySettingsViewModel settings)
        {
        
        }
    }
}