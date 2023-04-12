using BSMediator.Core.Models;
using BSMediator.Core.Services.User;
using IMS.Core.Shared;
using Microsoft.AspNetCore.Http;
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
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthApiService _authService;

        public AuthController(IAuthApiService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult>> Login(LoginModel loginModel)
        {
            return ModelState.IsValid ? Ok(await _authService.ValidateUserApiAsync(loginModel)) : BadRequest(new ResponseResult("ModelError", ApiStatusCodeEnum.BadRequest));
        }
    }
}
