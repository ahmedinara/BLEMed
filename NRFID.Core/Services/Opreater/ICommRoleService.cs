using BSMediator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreater
{
    public interface ICommRoleService
    {
        #region Get
        Task<CommRoleParent> GetCommRole();
        #endregion

        #region Add
        Task<string> AddCommRole(List<CommRoleModel> commRoleModels);
        #endregion
    }
}
