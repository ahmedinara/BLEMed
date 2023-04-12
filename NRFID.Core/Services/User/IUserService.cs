using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.User
{
    public interface IUserService
    {
        #region Get
        Task<ResponseResult> GetAllUsers(string emp_code, string first_name, string last_name, string department, string app_status, string pageUrl, string pageNo, string token);
        Task<ResponseResult> GetUserById(int employeeId, string token);
        Task<ResponseResult> GetCard(int employeeId, string token);
        #endregion


    }
}
