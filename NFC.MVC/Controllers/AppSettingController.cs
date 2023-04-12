using BSMediator.Core;
using BSMediator.Core.Entities;
using BSMediator.Core.Filter;
using BSMediator.Core.Models;
using BSMediator.Core.Services.Opreater;
using BSMediator.Core.Services.Opreation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSMediator.FrontEnd.Controllers
{
    public class AppSettingController : Controller
    {
        private readonly IAppSettingService _appSettingService;
        private readonly IEmailService _emailService;
        private readonly ICommRoleService _commRoleService;
        public AppSettingController(IAppSettingService appSettingService, IEmailService emailService, ICommRoleService commRoleService)
        {
            _appSettingService = appSettingService;
            _emailService = emailService;
            _commRoleService = commRoleService;
        }
       
        [HttpGet]
        [Route("setting/connectionRole")]
        [AllowUserFilter((int)ActionEnum.CommunicationRules)]
        public async Task<IActionResult> ConnectionRole()
        {
            return View();
        }
        [HttpGet]
        [AllowUserFilter((int)ActionEnum.CommunicationRules)]

        public async Task<string> ConnectionRoleData()
        {
            return JsonConvert.SerializeObject(await _commRoleService.GetCommRole());
        }
        public async Task<string> PostConnectionRole([FromBody] List<CommRoleModel> commRoleModels)
        {
            return JsonConvert.SerializeObject(await _commRoleService.AddCommRole(commRoleModels));
        }
        public async Task<string> PostData([FromBody] SystemConfigrationWithDitales systemConfigrationWithDitales)
        {
            return JsonConvert.SerializeObject(await _appSettingService.UpdateSystemConfigration(systemConfigrationWithDitales));
        }
        public async Task<string> GetData(int? systemType)
        {
            return JsonConvert.SerializeObject(await _appSettingService.GetSystemConfigration(systemType));
        }
        [HttpGet]
        [AllowUserFilter((int)ActionEnum.IntegrationSystemSetting)]
        [Route("setting/integrationSystem")]
        public async Task<IActionResult> Edit()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowUserFilter((int)ActionEnum.IntegrationSystemSetting)]
        public async Task<IActionResult> Edit(AppSettingUrl appSettingUrl)
        {
            var appSetting = await _appSettingService.UdateAppSettingUrl(appSettingUrl,AppSettingGroupEnum.ApiConfigration);
            if(appSetting== null)
            return View(appSettingUrl);
            else
              return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        [Route("setting/mailSetting")]
        [AllowUserFilter((int)ActionEnum.MailSetting)]

        public async Task<IActionResult> MailSetting()
        {
            var appSettingUrl =await _appSettingService.GetAppSettingMail();
            return View(appSettingUrl);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("setting/mailSetting")]
        [AllowUserFilter((int)ActionEnum.MailSetting)]

        public async Task<IActionResult> MailSetting(AppSettingMail appSettingMail)
        {
            var appSetting = await _appSettingService.UdateAppSettingMail(appSettingMail);
            return View(appSetting);
        }

        [HttpPost]
        public async Task<string> SendMail([FromBody] MailBody mailBody)
        {
            var isSent = await _emailService.SendEmailAsync(mailBody.to,mailBody.Subject,mailBody.body,mailBody.cc,mailBody.bcc,null,true);
            return JsonConvert.SerializeObject(isSent);
        }
    }
}
