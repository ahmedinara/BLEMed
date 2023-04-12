using NFC.Core.Models;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreation
{
    public interface IAuthAdminService
    {
        #region Hash Salt Password
        string CreateRandomHasheSaltPassword(out string textPassword, out byte[] salt);
        string CreateHashSaltPassword(string password, out byte[] salt);
        string GetHashSaltPassword(string password, byte[] salt);
        #endregion

        #region Validate Users
        Task<ResponseResult> ValidateUserAsync(LoginModel loginModel);
        #endregion
    }
}
