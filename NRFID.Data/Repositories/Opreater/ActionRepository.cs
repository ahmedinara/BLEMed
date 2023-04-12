using BSMediator.Core.Entities;
using BSMediator.Core.Repositories.Opreater;
using Microsoft.EntityFrameworkCore;
using NFC.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Data.Repositories.Opreater
{
    public class ActionRepository : IActionRepository
    {
        private readonly AppDbContext _dbContext;

        public ActionRepository(AppDbContext appContext)
        {
            _dbContext = appContext;
        }

        #region Get
        public async Task<IEnumerable<BSMediator.Core.Entities.Action>> GetActionAsync()
        {
            return await _dbContext.Actions.ToListAsync();
        }
        public async Task<bool> CheckActionByidAsync(int actionId)
        {
            return await _dbContext.Actions.AsNoTracking().AnyAsync(a=>a.Id==actionId);
        }
        public async Task<IEnumerable<BSMediator.Core.Entities.UserAction>> GetUserActionAsync(int userId)
        {
            return await _dbContext.UserActions.Include(u=>u.Action).Where(x => x.UserId == userId).ToListAsync();
        }
        public bool CheckUserActionIsActive(int userId,int[] actionId)
        {
            try
            {
                return  _dbContext.UserActions.Any(u => u.UserId == userId && actionId.Contains(u.ActionId) && u.IsActive == true);

            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return false;
            }

        }
        #endregion

        #region Add
        public async Task<BSMediator.Core.Entities.Action> AddActionAsync(BSMediator.Core.Entities.Action action)
        {
            var entity = await _dbContext.AddAsync(action);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        public async Task<IEnumerable<BSMediator.Core.Entities.UserAction>> AddUpdateUserActionAsync(List<UserAction> userActions, int userId)
        {
            try
            {
                foreach (var userAction in userActions)
                {
                    var ExistUserAction = await _dbContext.UserActions.AsNoTracking().FirstOrDefaultAsync(a => a.UserId == userAction.UserId && a.ActionId == userAction.ActionId);
                    if (ExistUserAction != null)
                    {
                        ExistUserAction.IsActive = userAction.IsActive;
                        _dbContext.UserActions.Update(ExistUserAction);
                    }
                    else
                    {
                        await _dbContext.UserActions.AddAsync(userAction);
                    }
                    await _dbContext.SaveChangesAsync();
                }

                return await _dbContext.UserActions.Where(u => u.UserId == userId).ToListAsync();
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }



        }
        #endregion

        #region Update

        #endregion

        #region Delete

        #endregion
    }
}
