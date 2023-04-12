using BSMediator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Repositories.Opreations
{
    public interface IUserRepository
    {
        #region Add
        Task<User> AddUser(User user);
        #endregion

        #region Get
        Task<IEnumerable<User>> GetUsers(string fristName, string lastName, bool? isActive, string email, string phone);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetActiveUserByEmailAsync(string email);
        Task<User> GetUserById(int id);
        Task<bool> CheckUserById(int id);
        #endregion

        #region Update
        Task<BSMediator.Core.Entities.User> UpdateUser(BSMediator.Core.Entities.User user);
        #endregion
    }
}
