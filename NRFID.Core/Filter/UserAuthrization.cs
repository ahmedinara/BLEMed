using IMS.Core.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSMediator.Core
{
    public class UserAuthrization : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            string isAuthenticated = context.HttpContext.Session.GetString("Token");
            string Type = context.HttpContext.Session.GetString("Type");
            if (Type == ((int)UserTypeEnum.Admin).ToString())
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                   new { action = "Login", controller = "Auth" }));
            if (isAuthenticated == null)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                   new { action = "Login", controller = "Auth" }));
            //To do : before the action executes  
            else
                await next();
            //To do : after the action executes  
        }
    }
}

