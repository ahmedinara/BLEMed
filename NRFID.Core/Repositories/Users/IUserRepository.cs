using BSMediator.Core.Models;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Repositories.Users
{
    public interface IUserRepository
    {

        #region Get 
        Task<EmplyeeList> GetUsers(string emp_code, string first_name, string last_name, string department, string app_status, string token, string pageNo="1");
        Task<Datum> GetUserById(int personId, string token);
        #endregion

        #region Update
        #endregion
    }
}
