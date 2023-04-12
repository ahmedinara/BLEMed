using BSMediator.Core.Enums;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreations;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Shared;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NFC.Api.Entities;
using NFC.Core.Enums;
using NFC.Core.Models;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.Opreation
{
    public class AuthAdminService : IAuthAdminService
    {
        private readonly IAppSettingService _appSettingService;
        private readonly IUserRepository _user;
        private readonly HttpClientService _httpClientService;

        public AuthAdminService(IAppSettingService appSettingService, IUserRepository userRepository, HttpClientService httpClientService)
        {
            _user = userRepository;
            _appSettingService = appSettingService;
            _httpClientService = httpClientService;
        }

        #region Hash Salt Password
        public string CreateRandomHasheSaltPassword(out string textPassword, out byte[] salt)
        {
            Random randomNumber = new Random();
            textPassword = GeneralStringFunctions.GeneratePassword(10);
            return CreateHashSaltPassword(textPassword, out salt);
        }

        public string CreateHashSaltPassword(string password, out byte[] salt)
        {
            // generate a 128-bit salt using a secure PRNG
            salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashedSaltPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedSaltPassword;
        }

        public string GetHashSaltPassword(string password, byte[] salt)
        {

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }
        #endregion

        #region Create Token
        private async Task<string> CreateUserToken(int userId,string userName)
        {
            //set the time when it expires
            string expirationInMin = "2400";

            DateTime expires = DateTime.Now.AddMinutes(int.Parse(expirationInMin));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim("Secret",_appSettingService.GetSecret()));

            claimsIdentity.AddClaim(new Claim("UserId", userId.ToString()));
            claimsIdentity.AddClaim(new Claim("UserName", userName));


            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Messages.SecretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsIdentity),
                SigningCredentials = credentials,
                Expires = expires
            };
            IdentityModelEventSource.ShowPII = true;
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Validate Users
        public async Task<ResponseResult> ValidateUserAsync(LoginModel loginModel)
        {
            var user = await _user.GetActiveUserByEmailAsync(loginModel.username);
            if (user == null || GetHashSaltPassword(loginModel.password, user.PasswordSalt) != user.Password)
                return new ResponseResult(Messages.WrongPassword, ApiStatusCodeEnum.BadRequest);

            SystemConfigrationWithDitales app = await _appSettingService.GetSystemConfigration(null);
            Config.SystemType = int.Parse(app.SystemType);
            Config.ConnType = int.Parse(app.ConnectionType);
            if (Config.ConnType == (int)ConnTypeEnum.Database)
            {

                Config.DbServerName = app.dt.FirstOrDefault(d => d.Key == "ServerName").Value;
                Config.DataBaseName = app.dt.FirstOrDefault(d => d.Key == "DataBaseName").Value;
                Config.DbUserName = app.dt.FirstOrDefault(d => d.Key == "UserName").Value;
                Config.DbPassword = app.dt.FirstOrDefault(d => d.Key == "Password").Value;
                
            }
            else
            {
                Config.AppUrl = app.dt.FirstOrDefault(d => d.Key == "Url").Value + ":" + app.dt.FirstOrDefault(d => d.Key == "Port").Value;
                string url = Config.AppUrl + AppEndPoint.LoginEndPoint;
                Config.ApiPassword = app.dt.FirstOrDefault(d => d.Key == "Password").Value;
                Config.ApiUserName = app.dt.FirstOrDefault(d => d.Key == "UserName").Value;

                LoginModel apiLoginModel = new LoginModel 
                {
                    password = Config.ApiPassword,
                    username=Config.ApiUserName,
                }; 
                #region Validate User
                var userApi = await _httpClientService.CallServicePost(url, apiLoginModel, "");
                if (!userApi.IsSucceeded)
                    return userApi;
                #endregion
                UserToken userToken = JsonConvert.DeserializeObject<UserToken>((string)userApi.ReturnData);
                Config.ApiToken = userToken.token;
            }
            var userAdminToken =await CreateUserToken(user.Id, user.FristName + " " + user.LastName);
            return new ResponseResult(new UserToken {token=userAdminToken }, ApiStatusCodeEnum.OK);
        }
        #endregion

    }
}
