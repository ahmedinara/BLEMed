using BSMediator.Core.Entities;
using BSMediator.Core.Models;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Services.User;
using BSMediator.Core.Shared;
using IMS.Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NFC.Core.Enums;
using NFC.Core.Models;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFC.MVC.Controllers
{

    public class AuthController : Controller
    {
        private readonly IAuthApiService _authUserService;
        private readonly IAuthAdminService _authAdminService;
        private readonly IAppSettingService _appSettingService;


        public AuthController(IAuthApiService authService, IAuthAdminService authAdminService, IAppSettingService appSettingService)
        {
            _authUserService = authService;
            _authAdminService = authAdminService;
            _appSettingService = appSettingService;
        }

        [HttpGet]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            ResponseResult authResult = new ResponseResult();
            bool isAdmin = false;
            if (!ModelState.IsValid) return View(loginModel);

            authResult = await _authAdminService.ValidateUserAsync(loginModel);
            if (!authResult.IsSucceeded)
            {
                ModelState.AddModelError("Password", authResult.ErrorMessage);
                return View(loginModel);
            }
            var userToken = (UserToken)authResult.ReturnData;
            //HttpContext.Session.SetString("Type", ((int)UserTypeEnum.Admin).ToString());
            //HttpContext.Session.SetString("UserName", user.FristName + " " + user.LastName);
            if (!authResult.IsSucceeded)
            {
                ModelState.AddModelError("Password", authResult.ErrorMessage);
                return View(loginModel);
            }
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddYears(10);
            /*Token*/
            Response.Cookies.Append("Token", userToken.token, option);

            return RedirectToAction("Index", "Home");

            #region Comment
            //if (loginModel.Usertype == (int)UserTypeEnum.Admin)
            //{
            //}
            //else if (loginModel.Usertype == (int)UserTypeEnum.User)
            //{
            //    authResult = await _authUserService.ValidateUserAsync(loginModel);
            //    if (!authResult.IsSucceeded)
            //    {
            //        ModelState.AddModelError("Password", authResult.ErrorMessage);
            //        return View(loginModel);
            //    }
            //    UserToken userToken = (UserToken)authResult.ReturnData;
            //    CookieOptions option = new CookieOptions();
            //    option.Expires = DateTime.Now.AddMinutes(10);
            //    Response.Cookies.Append("Token", userToken.token, option);

            //    HttpContext.Session.SetString("Type", ((int)UserTypeEnum.User).ToString());
            //    HttpContext.Session.SetString("Token", userToken.token);
            //    HttpContext.Session.SetString("UserName", loginModel.username);

            //}
            //else
            //{
            //    ModelState.AddModelError("Usertype", Messages.ErrorUsertype);
            //    return View(loginModel);
            //}
            #endregion
        }
    }
}
