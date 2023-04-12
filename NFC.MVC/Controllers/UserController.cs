using BSMediator.Core;
using BSMediator.Core.Filter;
using BSMediator.Core.Models;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Services.User;
using BSMediator.Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSMediator.FrontEnd.Controllers
{
    [AllowUserFilter((int)ActionEnum.Employee)]

    public class UserController : Controller
    {
        private readonly IAppSettingService _appSetting;
        private readonly IUserService _userService;
        private readonly BSMediator.Core.Services.Opreater.ICommencationHandlerService _commencationHandlerService;

        public UserController(IUserService userService, IAppSettingService appSetting, BSMediator.Core.Services.Opreater.ICommencationHandlerService commencationHandlerService)
        {
            _appSetting = appSetting;
            _userService = userService;
            _commencationHandlerService = commencationHandlerService;
        }

        
        public async Task<string> GetData(string emp_code, string first_name, string last_name, string department, string app_status, string pageUrl, string pageNo)
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var iDisplayLength = Convert.ToInt32(HttpContext.Request.Form["length"]);/// Number of records that should be shown in table
            var iDisplayStart = Convert.ToInt32(HttpContext.Request.Form["start"]);/// First record that should be shown(used for paging)
            var search = HttpContext.Request.Form["search[value]"].FirstOrDefault();//used for filtering

            if (string.IsNullOrEmpty(pageUrl))
            {
                pageUrl = Config.AppUrl + AppEndPoint.EmployeEndPoint;
            }

            var result = await _userService.GetAllUsers(emp_code, first_name, last_name, department, app_status, pageUrl, draw.ToString(), Config.ApiToken);
            if (!result.IsSucceeded)
                return "0";
            var emplyeeList = (EmplyeeList)result.ReturnData;


            var json = new
            {
                draw = draw.ToString(),
                recordsTotal = emplyeeList.count,
                recordsFiltered = emplyeeList.count,
                data = emplyeeList.data
            };

            return JsonConvert.SerializeObject(json);
        }
        
        [AllowUserFilter((int)ActionEnum.Employee)]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [AllowUserFilter((int)ActionEnum.EmployeeDetial)]
        public async Task<IActionResult> Details(int id)
        {
            var user = (Datum)(await _userService.GetUserById(id, Config.ApiToken)).ReturnData;
            return View(user);
        }

        [HttpPost]
        public IActionResult Details(int id, [FromBody] string CardNo)
        {
            //var user = (User)_userService.GetUserById(id).ReturnData;
            //ViewData["Code"] = user.CardNo;
            return View();
        }

        [HttpGet]
        public async Task<string> GetCardKey(int id)
        {
            var result = await _userService.GetCard(id, Config.ApiToken);
            if (!result.IsSucceeded)
                return "0";
            await _commencationHandlerService.Send((int)CommRoleActionEnum.GenrateCode, new MailBody());
            return JsonConvert.SerializeObject(result.ReturnData);
        }

    }
}
