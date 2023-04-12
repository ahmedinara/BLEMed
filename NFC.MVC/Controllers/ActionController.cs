using BSMediator.Core.Services.Opreater;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BSMediator.Core.Models;
using System.Collections.Generic;
using BSMediator.Core.Filter;
using NFC.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace BSMediator.FrontEnd.Controllers
{

    public class ActionController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IActionService _actionService;
        private readonly Core.Services.Opreater.IUserService _userService;
        public ActionController(IActionService actionService, Core.Services.Opreater.IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _actionService = actionService;
            _userService = userService;
        }
        public async Task<string> GetData(string userId)
        {
            var result = (await _actionService.GetUserActionAsync(int.Parse(userId))).ToList();
          
            return JsonConvert.SerializeObject(result);
        }
        public async Task<string> PostData([FromBody] List<UserActionModel> userActionModels)
        {
            var result = await _actionService.AddUpdateUserActionAsync(userActionModels, userActionModels.First().UserId);
            return "1";
        }
        [AllowUserFilter((int)ActionEnum.UserPermissions)]

        public async Task<IActionResult> Index()
        {
            int userId =int.Parse(_httpContextAccessor.HttpContext.Session.GetString("UserId"));
            ViewData["Users"] = new SelectList((await _userService.GetDLL()).ToList(), "Id", "FullName", userId);

            return View();
        }
        public async Task<IActionResult> UnAuthorized()
        {
            return View();
        }
    }
}
