using BSMediator.Core.ResourceFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSMediator.Core.Shared
{
    public class LanguageFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
             var language = context.HttpContext.Session.GetString("lang");
            if (language == "ar")
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Messages.LangAr);
                AppEndPoint.Lang = Messages.LangAr;

            }
            else
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(Messages.LangEn);
                AppEndPoint.Lang = Messages.LangEn;

            }
            // execute any code before the action executes
            var result = await next();
             
            // execute any code after the action executes
        }
    }

}
