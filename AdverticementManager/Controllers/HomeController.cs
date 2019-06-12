using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AdverticementManager.Models;
using AdverticementManager.Utils;
using AdverticementManager.ViewModels;
using AdvertisementProfiles.VK;
using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AdverticementManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IVkHelper _vkHelper;
        private readonly IMapper _mapper;

        public HomeController(UserManager<User> userManager, IConfiguration configuration, IVkHelper vkHelper, IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _vkHelper = vkHelper;
            _mapper = mapper;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            var vm = new MarketerIndexViewModel
            {
                VkAuthUrl = _configuration.GetValue<string>("VKOauthTokenUri")
            };

            if (roles.Contains("User", StringComparer.InvariantCultureIgnoreCase))
            {
                return View("MarketerIndex", vm);
            }

            return View(new IndexViewModel
            {
                CurrentUser = user,
                CurrentUserRoles = roles
            });
        }

        [HttpGet("/token")]
        public async Task<IActionResult> ParseToken(string code)
        {
            var accessToken = await _vkHelper.GetAccessToken(code);
            var profiles = await _vkHelper.GetAdProfiles(accessToken);
            var vm = _mapper.Map<VkAdProfilesViewModel>(profiles);
            return Content("");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
