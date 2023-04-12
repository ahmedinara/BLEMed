using BSMediator.Core.Models;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Core.Services.Opreation
{
    public interface IAppSettingService
    {
        #region Get
        Task<SystemConfigrationWithDitales> GetSystemConfigration(int? systemType);
        Task<SystemConfigration> GetSystemAsync();
        Task<AppSettingMail> GetAppSettingMail();
        Task<DataBaseConfigration> GetDataBaseConfigration(AppSettingGroupEnum appSettingGroupEnum);
        Task<AppSettingUrl> GetAppSettingUrl(AppSettingGroupEnum appSettingGroupEnum);
        string GetSecret();
        void GetSystemConfigration();
        #endregion

        #region Update
        Task<string> UpdateSystemConfigration(SystemConfigrationWithDitales systemConfigrationWithDitales);
        Task<SystemConfigration> UpdateSystemConfigration(SystemConfigration systemConfigration);
        Task<AppSettingUrl> UdateAppSettingUrl(AppSettingUrl appSettingUrl, AppSettingGroupEnum appSettingGroupEnum);
        Task<DataBaseConfigration> UdateDataBaseConfigration(DataBaseConfigration dataBaseConfigration, AppSettingGroupEnum appSettingGroupEnum);
        Task<AppSettingMail> UdateAppSettingMail(AppSettingMail appSettingMail);
        #endregion
    }
}
