using BSMediator.Core.Models;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Services.User;
using BSMediator.Core.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NFC.Core.Enums;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BSMediator.FrontEnd.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IAppSettingService _appSetting;
        private readonly IUserService _userService;
        public UserController(IUserService userService, IAppSettingService appSetting)
        {
            _appSetting = appSetting;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ResponseResult> GetData(string emp_code, string first_name, string last_name, string department, string app_status, string pageUrl, string pageNo,int draw=1)
        {
           
            if (string.IsNullOrEmpty(pageUrl))
            {
                var url =await _appSetting.GetAppSettingUrl(AppSettingGroupEnum.ApiConfigration);
                pageUrl = url.ApplictionUrl + ":" + url.ApplicationPort + AppEndPoint.EmployeEndPoint;
            }
            ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
            string userToken = identity.FindFirst("appToken").Value;

            var result = await _userService.GetAllUsers(emp_code, first_name, last_name, department, app_status, pageUrl, draw.ToString(), userToken);
            if (!result.IsSucceeded)
                return result;

            var emplyeeList = (EmplyeeList)result.ReturnData;

            var json = new PagedList<Datum>(emplyeeList.data.ToList(), emplyeeList.count,draw,emplyeeList.count);

            return new ResponseResult(json,ApiStatusCodeEnum.OK);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {

            var url =await _appSetting.GetAppSettingUrl(AppSettingGroupEnum.ApiConfigration);
            string pageUrl = url.ApplictionUrl + ":" + url.ApplicationPort + AppEndPoint.EmployeEndPoint + id + "/";

            string userToken = HttpContext.Session.GetString("Token");
            var user = (Datum)(await _userService.GetUserById(id, userToken)).ReturnData;
            return View(user);
        }
       
        [HttpGet("card-key/{id}")]
        public async Task<string> GetCardKey(int id)
        {
            var url =await _appSetting.GetAppSettingUrl(AppSettingGroupEnum.ApiConfigration);
            string pageUrl = url.ApplictionUrl + ":" + url.ApplicationPort + AppEndPoint.EmployeEndPoint + id + "/";

            string userToken = HttpContext.Session.GetString("Token");
            var result = await _userService.GetCard(id, userToken);
            if (!result.IsSucceeded)
                return "0";
            return JsonConvert.SerializeObject(result.ReturnData);

        }
    }
}
