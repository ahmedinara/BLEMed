using BSMediator.Core;
using BSMediator.Core.Filter;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.Opreater;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NFC.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NFC.MVC.Controllers
{
    [AllowUserFilter(0)]
    public class HomeController : Controller
    {
        private readonly IActionService _actionService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IActionService actionService)
        {
            _logger = logger;
            _actionService = actionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            if (CultureInfo.CurrentCulture.Name == "en-US")
                culture = "ar-SA";
            else
                culture = "en-US";

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    }
            );

            return LocalRedirect(returnUrl);
        }
        public IActionResult lang()
        {
            HttpContext.Session.SetString("lang", "ar");
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
