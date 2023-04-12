using Microsoft.Extensions.Configuration;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NFC.Api.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BSMediator.Core.Repositories.Opreater;
using Microsoft.AspNetCore.Routing;
using BSMediator.Core.Repositories.Opreation;
using Microsoft.AspNetCore.Http;
using BSMediator.Core.Shared;
using BSMediator.Core.Services.Opreation;

namespace BSMediator.Core.Filter
{
    public class AllowUserFilter : TypeFilterAttribute
    {
        public AllowUserFilter(params int[] actionType) : base(typeof(CheckUserType))
        {
            Arguments = new object[] { actionType };
        }

        private class CheckUserType : IActionFilter
        {
            private readonly int[] _actionType;
            private readonly IAppSettingService _appSettingService;
            private readonly IActionRepository _actionRepository;
            public CheckUserType(int[] actionType, IActionRepository actionRepository, IAppSettingService appSettingService)
            {
                _appSettingService = appSettingService;
                _actionType = actionType;
                _actionRepository = actionRepository;
            }
            public async void OnActionExecuting(ActionExecutingContext context)
            {
                try
                {
                    string userId = "0";
                    string token = context.HttpContext.Request.Cookies["Token"];
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token);
                    var tokenS = jsonToken as JwtSecurityToken;
                    if (Config.SystemType == 0)
                    {
                        _appSettingService.GetSystemConfigration();
                    }
                    if (token != null)
                    {
                        
                        userId = tokenS.Claims.First(c => c.Type == "UserId").Value.ToString();
                        context.HttpContext.Session.SetString("UserName", tokenS.Claims.First(c => c.Type == "UserName").Value.ToString());
                        context.HttpContext.Session.SetString("UserId", tokenS.Claims.First(c => c.Type == "UserId").Value.ToString());

                        if (tokenS.Claims.First(c => c.Type == "Secret").Value.ToString() != _appSettingService.GetSecret())
                        {
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Auth" },
                                          { "action", "login" }
                                         });
                        }
                    }
                    if (!_actionType.Any(s => s == 0))
                    {
                        var hasAccess = _actionRepository.CheckUserActionIsActive(int.Parse(userId), _actionType);
                        if (!hasAccess)
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Action" },
                                          { "action", "UnAuthorized" }
                                         });
                       
                    }


                }
                catch (Exception ex)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary{{ "controller", "Auth" },
                                          { "action", "login" }
                                         });
                }


            }
            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}
