using BSMediator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Repositories.Opreater
{
    public interface IActionRepository
    {
        #region Get
        Task<IEnumerable<BSMediator.Core.Entities.Action>> GetActionAsync();
        Task<IEnumerable<BSMediator.Core.Entities.UserAction>> GetUserActionAsync(int userId);
        Task<bool> CheckActionByidAsync(int actionId);
        bool CheckUserActionIsActive(int userId, int[] actionId);
        #endregion

        #region Add
        Task<BSMediator.Core.Entities.Action> AddActionAsync(BSMediator.Core.Entities.Action action);
        Task<IEnumerable<BSMediator.Core.Entities.UserAction>> AddUpdateUserActionAsync(List<UserAction> userActions, int userId);
        #endregion
    }
}
