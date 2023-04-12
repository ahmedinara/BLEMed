
using BSMediator.Core.Enums;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreations;
using BSMediator.Core.Repository;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.User;
using BSMediator.Core.Shared;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

namespace BSMediator.Service.Services.User
{
    public class AuthApiService : IAuthApiService
    {
        private readonly BSMediator.Core.Services.Opreation.IAppSettingService _appSettingService;
        private readonly HttpClientService _httpClientService;
        private readonly IUserRepository _user;

        public AuthApiService(BSMediator.Core.Services.Opreation.IAppSettingService appSettingService, HttpClientService httpClientService, IUserRepository userRepository)
        {
            _httpClientService = httpClientService;
            _appSettingService = appSettingService;
            _user = userRepository;
        }

        #region Hash Salt Password
        /// <summary>
        /// This Function Create Random Hashe Salt Password
        /// </summary>
        /// <param name="textPassword">Plan text Password</param>
        /// <param name="salt"></param>
        /// <returns>Hashe Salt Password</returns>
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
        private async Task<string> CreateUserToken(string aapToken, string userName, int systemType, int connType)
        {
            try
            {
                //set the time when it expires
                string expirationInMin = "2400";

                DateTime expires = DateTime.Now.AddMinutes(int.Parse(expirationInMin));
                ClaimsIdentity claimsIdentity = new ClaimsIdentity();

                claimsIdentity.AddClaim(new Claim("appToken", aapToken));
                claimsIdentity.AddClaim(new Claim("userName", userName));
                claimsIdentity.AddClaim(new Claim("systemType", systemType.ToString()));
                claimsIdentity.AddClaim(new Claim("connType", connType.ToString()));
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
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }

        }
        #endregion

        #region Validate Users
        /// <summary>
        /// Validate  User
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>

        public async Task<ResponseResult> ValidateUserApiAsync(LoginModel loginModel)
        {
            //AppSettingUrl appSettingUrl =await _appSettingService.GetAppSettingUrl(AppSettingGroupEnum.ApiConfigration);
            //string url = appSettingUrl.ApplictionUrl + ":" + appSettingUrl.ApplicationPort + AppEndPoint.LoginEndPoint;
            //#region Validate User
            //var user = _httpClientService.CallServicePost(url, loginModel, "");
            //if (!user.IsSucceeded)
            //    user.ErrorMessage = Messages.WrongPassword;
            //#endregion
            //UserToken userToken = JsonConvert.DeserializeObject<UserToken>((string)user.ReturnData);
            //userToken.token = await CreateUserToken(userToken.token,loginModel.username,);
            return new ResponseResult(null, ApiStatusCodeEnum.OK);
        }
        public async Task<ResponseResult> ValidateUserAsync(LoginModel loginModel)
        {
            SystemConfigrationWithDitales app = await _appSettingService.GetSystemConfigration(null);
            Config.SystemType = int.Parse(app.SystemType);
            Config.ConnType = int.Parse(app.ConnectionType);
            if (Config.ConnType == (int)ConnTypeEnum.Database)
            {
                var user = await _user.GetActiveUserByEmailAsync(loginModel.username);
                if (user == null || GetHashSaltPassword(loginModel.password, user.PasswordSalt) != user.Password)
                    return new ResponseResult(Messages.WrongPassword, ApiStatusCodeEnum.BadRequest);

                Config.DbServerName = app.dt.FirstOrDefault(d => d.Key == "ServerName").Value;
                Config.DataBaseName = app.dt.FirstOrDefault(d => d.Key == "DataBaseName").Value;
                Config.DbUserName = app.dt.FirstOrDefault(d => d.Key == "UserName").Value;
                Config.DbPassword = app.dt.FirstOrDefault(d => d.Key == "Password").Value;
                UserToken userToken = new UserToken();
                userToken.token = await CreateUserToken("", user.FristName + " " + user.LastName, Config.SystemType, Config.ConnType);
                return new ResponseResult(userToken, ApiStatusCodeEnum.OK);
            }
            else
            {
                Config.AppUrl = app.dt.FirstOrDefault(d => d.Key == "Url").Value + ":" + app.dt.FirstOrDefault(d => d.Key == "Port").Value;
                string url = Config.AppUrl + AppEndPoint.LoginEndPoint;
                #region Validate User
                var user =await _httpClientService.CallServicePost(url, loginModel, "");
                if (!user.IsSucceeded)
                    return user;



                #endregion
                UserToken userToken = JsonConvert.DeserializeObject<UserToken>((string)user.ReturnData);
                userToken.token = await CreateUserToken(userToken.token, loginModel.username, Config.SystemType, Config.ConnType);
                return new ResponseResult(userToken, ApiStatusCodeEnum.OK);
            }

        }
        #endregion

    }
}
