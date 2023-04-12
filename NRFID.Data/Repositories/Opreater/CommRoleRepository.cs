using BSMediator.Core.Entities;
using BSMediator.Core.Repositories.Opreater;
using BSMediator.Core.Services.Opreation;
using Microsoft.EntityFrameworkCore;
using NFC.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Data.Repositories.Opreater
{
    public class CommRoleRepository : ICommRoleRepository
    {

        private readonly AppDbContext _dbContext;

        public CommRoleRepository(AppDbContext appContext)
        {
            _dbContext = appContext;
        }


        #region Get
        public async Task<List<CommRole>> GetCommRoleWithDetialsAsync()
        {
            return await _dbContext.CommRoles.Include(d => d.commRoleDetials).ThenInclude(c => c.ApplicationSetting).ToListAsync();
        }
        public async Task<CommRole> GetCommRoleWithDetialsAsync(int id)
        {
            try
            {
               var commRole = await _dbContext.CommRoles.Include(d => d.commRoleDetials).ThenInclude(c => c.ApplicationSetting).FirstOrDefaultAsync(f => f.Id == id);
                
                return commRole;
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }
        }
        public async Task<List<CommRole>> GetCommRoleAsync()
        {
            return await _dbContext.CommRoles.ToListAsync();
        }
        public async Task<CommRole> GetCommRoleByIdAsync(int id)
        {
            return await _dbContext.CommRoles.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        #endregion

        #region Add
        public async Task<List<CommRole>> AddUpdateCommRoleWithDetialsAsync(List<CommRole> commRoles)
        {
            foreach (var item in commRoles)
            {
                var entity = await _dbContext.CommRoles.AsNoTracking().FirstOrDefaultAsync(f => f.ActionName == item.ActionName);
                foreach (var commRoleDetial in item.commRoleDetials)
                {
                    commRoleDetial.CommRoleId = entity.Id;
                    commRoleDetial.ApplicationSetting = null;
                    var commRoleDet = await _dbContext.CommRoleDetials.AsNoTracking().FirstOrDefaultAsync(c => c.TypeId == commRoleDetial.TypeId && c.CommRoleId == entity.Id);
                    if (commRoleDet ==null)
                    {
                        await _dbContext.CommRoleDetials.AddAsync(commRoleDetial);
                        await _dbContext.SaveChangesAsync();
                    }

                    else
                    {
                        commRoleDetial.Id = commRoleDet.Id;
                        _dbContext.CommRoleDetials.Update(commRoleDetial);
                        await _dbContext.SaveChangesAsync();

                    }

                }

            }
            await _dbContext.SaveChangesAsync();
            return commRoles;
        }
        #endregion
    }
}
