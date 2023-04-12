using BSMediator.Core.Entities;
using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Repository;
using Microsoft.EntityFrameworkCore;
using NFC.Api.Entities;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Data.Repositories.Opreation
{
    public class AppSettingRepository : IAppSettingRepository
    {
        private readonly AppDbContext _dbContext;

        public AppSettingRepository(AppDbContext appContext)
        {
            _dbContext = appContext;
        }

        #region add
        public async Task<UserAccessLog> AddLogAsync(UserAccessLog userAccessLog)
        {
            var entity = await _dbContext.UserAccessLogs.AddAsync(userAccessLog);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        #endregion

        #region Get
        public async Task<IEnumerable<ApplicationSetting>> GetApplicationSettingsByGroupIdAsync(int groupId)
        {
            return await _dbContext.ApplicationSettings.Where(a => a.GroupId == groupId).AsNoTracking().ToListAsync();
        }
        public IEnumerable<ApplicationSetting> GetApplicationSettingsByGroupId(int groupId)
        {
            return  _dbContext.ApplicationSettings.Where(a => a.GroupId == groupId).AsNoTracking().ToList();
        }

        public ApplicationSetting GetApplicationSettingById(int id)
        {
            return _dbContext.ApplicationSettings.AsNoTracking().FirstOrDefault(a => a.Id == id);
        }
        public string GetSecritKey()
        {
            return _dbContext.ApplicationSettings.AsNoTracking().FirstOrDefault(f => f.GroupId == (int)AppSettingGroupEnum.TokenConfig && f.Key == "SecretKey").Value;
        }
        #endregion

        #region Update
        public async Task<bool> UpdateSecritKey(string newSecret)
        {
            try
            {
                var applicationSetting = _dbContext.ApplicationSettings.FirstOrDefault(f => f.GroupId == (int)AppSettingGroupEnum.TokenConfig && f.Key == "SecretKey");
                applicationSetting.Value = newSecret;
                _dbContext.ApplicationSettings.Update(applicationSetting);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
         

        }
        public async Task<IEnumerable<ApplicationSetting>> UpdateApplicationSettingsByGroupIdAsync(int groupId, List<ApplicationSetting> applicationSettings)
        {
            try
            {
                var application = await _dbContext.ApplicationSettings.Where(x => x.GroupId == groupId).AsNoTracking().ToListAsync();
                if (application.Count() > 0)
                    _dbContext.ApplicationSettings.UpdateRange(applicationSettings);
                else
                    await _dbContext.ApplicationSettings.AddRangeAsync(applicationSettings);
                await _dbContext.SaveChangesAsync();
                return await _dbContext.ApplicationSettings.Where(a => a.GroupId == groupId).ToListAsync();
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }

        }
        public async Task<IEnumerable<ApplicationSetting>> UpdateApplicationSettingsByGroupIdAsync(int[] groupId, List<ApplicationSetting> applicationSettings)
        {
            try
            {
                var application = await _dbContext.ApplicationSettings.Where(x => groupId.Contains(x.GroupId)).AsNoTracking().ToListAsync();
                if (application.Count() > 0)
                    _dbContext.ApplicationSettings.UpdateRange(applicationSettings);
                else
                    await _dbContext.ApplicationSettings.AddRangeAsync(applicationSettings);
                await _dbContext.SaveChangesAsync();
                return await _dbContext.ApplicationSettings.Where(a => groupId.Contains(a.GroupId)).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                var x = ex.ToString();
                return null;
            }

        }

        #endregion
    }
}
