using BSMediator.Core.BioTimeEntites;
using BSMediator.Core.Entities;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Repositories.Users;
using BSMediator.Core.Repository;
using BSMediator.Core.ResourceFiles;
using BSMediator.Core.Services.User;
using BSMediator.Core.Shared;
using Core.Models;
using NFC.Core.Enums;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.User
{
    public class UserService : IUserService
    {
        private readonly IAppSettingRepository _appSettingReposirtory;
        private readonly BSMediator.Core.Repositories.Users.IUserRepository _userRepository;

        public UserService(HttpClientService httpClientService, BioTimeContext bioTimeContext, IAppSettingRepository appSettingReposirtory)
        {
            _appSettingReposirtory = appSettingReposirtory;
            _userRepository = UserSwitcher.CreateUserInstance(Config.ConnType, httpClientService, bioTimeContext);
        }

        #region Get
        public async Task<ResponseResult> GetAllUsers(string emp_code, string first_name, string last_name, string department, string app_status, string pageUrl, string pageNo, string token)
        {
            EmplyeeList emplyeeList = await _userRepository.GetUsers(emp_code, first_name, last_name, department, app_status, token, pageNo);

            if (emplyeeList == null)
                return new ResponseResult(Messages.EmptyUserData, ApiStatusCodeEnum.NotFound);

            return new ResponseResult(emplyeeList, ApiStatusCodeEnum.OK);
        }
        public async Task<ResponseResult> GetUserById(int employeeId, string token)
        {
            var user = await _userRepository.GetUserById(employeeId, token);
            if (user == null)
                return new ResponseResult(Messages.NotFoundUser, ApiStatusCodeEnum.NotFound);

            return new ResponseResult(user, ApiStatusCodeEnum.OK);
        }
        public async Task<ResponseResult> GetCard(int employeeId, string token)
        {
            var user = await _userRepository.GetUserById(employeeId, token);
            if (user == null)
                return new ResponseResult(Messages.NotFoundUser, ApiStatusCodeEnum.NotFound);
            var card = EncriptCardNumber(new EncriptedModel(user.card_no));
            var applog = await _appSettingReposirtory.AddLogAsync(new UserAccessLog { CreatedAt = DateTime.UtcNow, CreatedBy = 1, UserId = user.id, EncriptedCode = card });
            return new ResponseResult(new CardNoEncripted { CardNoKey = card }, ApiStatusCodeEnum.OK);
        }
        private string EncriptCardNumber(EncriptedModel encriptedModel)
        {
            return ManagementEncryption.EncryptPassword(encriptedModel);
        }
        #endregion

    }
}
