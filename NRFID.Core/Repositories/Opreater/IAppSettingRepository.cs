using BSMediator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Repositories.Opreation
{
    public interface IAppSettingRepository
    {
        #region add
        Task<UserAccessLog> AddLogAsync(UserAccessLog userAccessLog);
        #endregion

        #region Get
        IEnumerable<ApplicationSetting> GetApplicationSettingsByGroupId(int groupId);
        Task<IEnumerable<ApplicationSetting>> GetApplicationSettingsByGroupIdAsync(int groupId);
        ApplicationSetting GetApplicationSettingById(int id);
        string GetSecritKey();
        #endregion

        #region Update
        Task<bool> UpdateSecritKey(string newSecret);
        Task<IEnumerable<ApplicationSetting>> UpdateApplicationSettingsByGroupIdAsync(int[] groupId, List<ApplicationSetting> applicationSettings);
        Task<IEnumerable<ApplicationSetting>> UpdateApplicationSettingsByGroupIdAsync(int groupId, List<ApplicationSetting> applicationSettings);
        #endregion
    }
}
