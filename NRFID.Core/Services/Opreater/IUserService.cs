using BSMediator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreater
{
    public interface IUserService
    {
        #region Add
        Task<BSMediator.Core.Entities.User> AddUser(BSMediator.Core.Entities.User user);
        #endregion

        #region Update
        Task<BSMediator.Core.Entities.User> UpdateUser(UserModel userModel, int userId, int updatedUser);
        #endregion

        #region Get
        Task<IEnumerable<UserDLL>> GetDLL();
        Task<UserModel> GetUserModelById(int id);
        Task<IEnumerable<BSMediator.Core.Entities.User>> GetAll();
        Task<BSMediator.Core.Entities.User> GetById(int id);
        #endregion
    }
}
