using BSMediator.Core.Models;
using BSMediator.Core.Services.Opreation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSMediator.FrontEnd.Controllers
{
    [Route("api/app-setting")]
    [ApiController]
    [Authorize]
    public class AppSettingController : Controller
    {
        private readonly IAppSettingService _appSettingService;

        public AppSettingController(IAppSettingService appSettingService)
        {
            _appSettingService = appSettingService;
        }

        [HttpGet]
        public async Task<AppSettingUrl> GetSettingUrlAsync()
        {
            var appSettingUrl =await _appSettingService.GetAppSettingUrl(AppSettingGroupEnum.ApiConfigration);
            return appSettingUrl;
        }
        [HttpPost]
        public async Task<AppSettingUrl> Edit(AppSettingUrl appSettingUrl)
        {
            AppSettingUrl appSetting = await _appSettingService.UdateAppSettingUrl(appSettingUrl,AppSettingGroupEnum.ApiConfigration);
            return appSetting;
        }
    }
}
