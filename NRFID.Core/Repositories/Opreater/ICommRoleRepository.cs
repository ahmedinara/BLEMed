using BSMediator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Repositories.Opreater
{
    public interface ICommRoleRepository
    {
        #region Get
        Task<List<CommRole>> GetCommRoleWithDetialsAsync();
        Task<List<CommRole>> GetCommRoleAsync();
        Task<CommRole> GetCommRoleByIdAsync(int id);
        Task<CommRole> GetCommRoleWithDetialsAsync(int id);
        #endregion

        #region Add
        Task<List<CommRole>> AddUpdateCommRoleWithDetialsAsync(List<CommRole> commRoles);
        #endregion
    }
}
