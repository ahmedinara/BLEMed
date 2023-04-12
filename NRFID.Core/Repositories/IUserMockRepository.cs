using BSMediator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Repository
{
    public interface IUserMockRepository
    {
        #region Add
        User AddUser(User user);
        #endregion

        #region Update

        #endregion

        #region Get
        IEnumerable<User> GetUsers(string fristName, string lastName, bool? isActive, string email, string phone);
        User GetActiveUserByEmailAsync(string email);
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        #endregion
    }
}
