using BSMediator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreater
{
    public interface IActionService
    {
        #region Get
        Task<IEnumerable<BSMediator.Core.Entities.Action>> GetActionAsync();
        Task<IEnumerable<UserActionWithActionNameModel>> GetUserActionAsync(int userId);
        bool CheckUserActionIsActive(int userId, int[] actionId);
        #endregion

        #region AddUpdate
        Task<BSMediator.Core.Entities.Action> AddActionAsync(BSMediator.Core.Entities.Action action);
        Task<IEnumerable<BSMediator.Core.Entities.UserAction>> AddUpdateUserActionAsync(List<UserActionModel> userActionModels, int userId);
        #endregion
    }
}
