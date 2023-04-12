using BSMediator.Core.Entities;
using BSMediator.Core.Enums;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Repository;
using BSMediator.Core.Services.Opreation;
using BSMediator.Core.Shared;
using NFC.Core.Enums;
using NFC.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.Opreation
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IAppSettingRepository _appSettingReposirtory;
        public AppSettingService(IAppSettingRepository appSettingReposirtory)
        {
            _appSettingReposirtory = appSettingReposirtory;
        }

        #region Get
        public async Task<SystemConfigration> GetSystemAsync()
        {
            var systemType = (await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.SystemType));
            if (systemType.Count() == 0)
                return new SystemConfigration();

            return new SystemConfigration(systemType.FirstOrDefault(f => f.Key == "SystemType").Value, systemType.FirstOrDefault(f => f.Key == "ConnectionType").Value);
        }
        private SystemConfigration GetSystem()
        {
            var systemType = ( _appSettingReposirtory.GetApplicationSettingsByGroupId((int)AppSettingGroupEnum.SystemType));
            if (systemType.Count() == 0)
                return new SystemConfigration();

            return new SystemConfigration(systemType.FirstOrDefault(f => f.Key == "SystemType").Value, systemType.FirstOrDefault(f => f.Key == "ConnectionType").Value);
        }
        
        public async Task<AppSettingMail> GetAppSettingMail()
        {
            var appSetting = await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.MailConfigration);
            if (appSetting.ToList().Count == 0)
                return new AppSettingMail();

            AppSettingMail appSettingMail = new AppSettingMail
            {
                Host = appSetting.FirstOrDefault(s => s.Key == "Host").Value,
                mailForm = appSetting.FirstOrDefault(s => s.Key == "From").Value,
                Password = appSetting.FirstOrDefault(s => s.Key == "Password").Value,
                Port = appSetting.FirstOrDefault(s => s.Key == "Port").Value
            };

            return appSettingMail;
        }
        public async Task<DataBaseConfigration> GetDataBaseConfigration(AppSettingGroupEnum appSettingGroupEnum)
        {
            var appSetting = await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)appSettingGroupEnum);
            if (appSetting.ToList().Count == 0)
                return new DataBaseConfigration();

            string serverName = appSetting.FirstOrDefault(x => x.Key == "ServerName").Value;
            string dataBaseName = appSetting.FirstOrDefault(x => x.Key == "DataBaseName").Value;
            string userName = appSetting.FirstOrDefault(x => x.Key == "UserName").Value;
            string password = appSetting.FirstOrDefault(x => x.Key == "Password").Value;

            return new DataBaseConfigration(serverName, dataBaseName, userName, password);
        }
        public async Task<AppSettingUrl> GetAppSettingUrl(AppSettingGroupEnum appSettingGroupEnum)
        {
            var appSetting = await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)appSettingGroupEnum);
            if (appSetting.Count() == 0)
                return new AppSettingUrl();

            return new AppSettingUrl
            {
                ApplictionUrl = appSetting.FirstOrDefault(x => x.Key == "Url").Value,
                ApplicationPort = appSetting.FirstOrDefault(x => x.Key == "Port").Value,
                UserName = appSetting.FirstOrDefault(x => x.Key == "UserName").Value,
                Password = appSetting.FirstOrDefault(x => x.Key == "Password").Value
            };
        }
        public async Task<SystemConfigrationWithDitales> GetSystemConfigration(int? systemType)
        {
            var systemConfigration = await GetSystemAsync();
            if (systemConfigration == null)
                return new SystemConfigrationWithDitales();
            SystemConfigrationWithDitales systemConfigrationWithDitales = new SystemConfigrationWithDitales { ConnectionType = systemConfigration.ConnectionType, SystemType = systemConfigration.SystemType };
            if (systemType != null && systemConfigration.SystemType != systemType.ToString())
                return new SystemConfigrationWithDitales();
            if (systemConfigration.ConnectionType == ((int)ConnTypeEnum.Database).ToString())
            {
                systemConfigrationWithDitales.dt = new List<SystemConfigrationDitales>();

                var databaseConfigration = await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.DatabaseConfigration);
                if (databaseConfigration.Count() == 0)
                    return systemConfigrationWithDitales;
                systemConfigrationWithDitales.dt.AddRange(databaseConfigration.Select(s => new SystemConfigrationDitales { Key = s.Key, Value = s.Value }));
            }
            else
            {
                systemConfigrationWithDitales.dt = new List<SystemConfigrationDitales>();
                var urlConfigration = await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.ApiConfigration);
                var adds = urlConfigration.Select(s => new SystemConfigrationDitales { Key = s.Key, Value = s.Value }).ToList();
                systemConfigrationWithDitales.dt.AddRange(adds);
            }
            return systemConfigrationWithDitales;
        }
        public string GetSecret()
        {
           return _appSettingReposirtory.GetSecritKey();
        }
        public void GetSystemConfigration()
        {
            var systemConfigration = GetSystem();
            if (systemConfigration == null)
                return;

            SystemConfigrationWithDitales systemConfigrationWithDitales = new SystemConfigrationWithDitales { ConnectionType = systemConfigration.ConnectionType, SystemType = systemConfigration.SystemType };
            
            if (systemConfigration.ConnectionType == ((int)ConnTypeEnum.Database).ToString())
            {
                systemConfigrationWithDitales.dt = new List<SystemConfigrationDitales>();

                var databaseConfigration =  _appSettingReposirtory.GetApplicationSettingsByGroupId((int)AppSettingGroupEnum.DatabaseConfigration);
                if (databaseConfigration.Count() == 0)
                    return ;
                systemConfigrationWithDitales.dt.AddRange(databaseConfigration.Select(s => new SystemConfigrationDitales { Key = s.Key, Value = s.Value }));
                DataBaseConfigration dataBaseConfigration = new DataBaseConfigration();
                dataBaseConfigration.ServerName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "ServerName").Value;
                dataBaseConfigration.DataBaseName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "DataBaseName").Value;
                dataBaseConfigration.UserName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "UserName").Value;
                dataBaseConfigration.Password = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Password").Value;
                Config.SystemType = int.Parse(systemConfigration.SystemType);
                Config.ConnType = int.Parse(systemConfigration.ConnectionType);
                Config.DbServerName = dataBaseConfigration.ServerName;
                Config.DataBaseName = dataBaseConfigration.DataBaseName;
                Config.DbUserName = dataBaseConfigration.UserName;
                Config.DbPassword = dataBaseConfigration.Password;
                Config.AppUrl = "";
                Config.ApiPassword = "";
                Config.ApiUserName = "";
            }
            else
            {
                systemConfigrationWithDitales.dt = new List<SystemConfigrationDitales>();
                var urlConfigration =  _appSettingReposirtory.GetApplicationSettingsByGroupId((int)AppSettingGroupEnum.ApiConfigration);
                var adds = urlConfigration.Select(s => new SystemConfigrationDitales { Key = s.Key, Value = s.Value }).ToList();
                systemConfigrationWithDitales.dt.AddRange(adds);
                AppSettingUrl appSettingUrl = new AppSettingUrl();
                appSettingUrl.ApplictionUrl = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Url").Value;
                appSettingUrl.ApplicationPort = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Port").Value;
                appSettingUrl.UserName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "UserName").Value;
                appSettingUrl.Password = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Password").Value;
                Config.SystemType = int.Parse(systemConfigration.SystemType);
                Config.ConnType = (int)ConnTypeEnum.Api;
                Config.AppUrl = appSettingUrl.ApplictionUrl + ":" + appSettingUrl.ApplicationPort;
                Config.ApiPassword = appSettingUrl.Password;
                Config.ApiUserName = appSettingUrl.UserName;
                Config.DbServerName = "";
                Config.DataBaseName = "";
                Config.DbUserName = "";
                Config.DbPassword = "";
            }
        }

        #endregion

        #region Update
        public async Task<string> UpdateSystemConfigration(SystemConfigrationWithDitales systemConfigrationWithDitales)
        {
            var systemConfigration = await UpdateSystemConfigration(new SystemConfigration(systemConfigrationWithDitales.SystemType, systemConfigrationWithDitales.ConnectionType));
            if (systemConfigration == null)
                return "0";
            if (systemConfigration.ConnectionType == ((int)ConnTypeEnum.Api).ToString())
            {
                AppSettingUrl appSettingUrl = new AppSettingUrl();
                appSettingUrl.ApplictionUrl = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Url").Value;
                appSettingUrl.ApplicationPort = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Port").Value;
                appSettingUrl.UserName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "UserName").Value;
                appSettingUrl.Password = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Password").Value;
                Config.SystemType = int.Parse(systemConfigration.SystemType);
                Config.ConnType = (int)ConnTypeEnum.Api;
                Config.AppUrl = appSettingUrl.ApplictionUrl + ":" + appSettingUrl.ApplicationPort;
                Config.ApiPassword = appSettingUrl.Password;
                Config.ApiUserName = appSettingUrl.UserName;
                Config.DbServerName = "";
                Config.DataBaseName = "";
                Config.DbUserName = "";
                Config.DbPassword = "";
                var appSettingUrlAdd = await UdateAppSettingUrl(appSettingUrl, AppSettingGroupEnum.ApiConfigration);
                Guid newSecret = Guid.NewGuid();
                _appSettingReposirtory.UpdateSecritKey(newSecret.ToString());
                if (appSettingUrlAdd != null)
                    return "1";
            }
            if (systemConfigration.ConnectionType == ((int)ConnTypeEnum.Database).ToString())
            {
                DataBaseConfigration dataBaseConfigration = new DataBaseConfigration();
                dataBaseConfigration.ServerName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "ServerName").Value;
                dataBaseConfigration.DataBaseName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "DataBaseName").Value;
                dataBaseConfigration.UserName = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "UserName").Value;
                dataBaseConfigration.Password = systemConfigrationWithDitales.dt.FirstOrDefault(x => x.Key == "Password").Value;
                Config.SystemType = int.Parse(systemConfigration.SystemType);
                Config.ConnType = int.Parse(systemConfigration.ConnectionType);
                Config.DbServerName = dataBaseConfigration.ServerName;
                Config.DataBaseName = dataBaseConfigration.DataBaseName;
                Config.DbUserName = dataBaseConfigration.UserName;
                Config.DbPassword = dataBaseConfigration.Password;
                Config.AppUrl = "";
                Config.ApiPassword = "";
                Config.ApiUserName = "";
                var dataBaseConfigrationAdd = await UdateDataBaseConfigration(dataBaseConfigration, AppSettingGroupEnum.DatabaseConfigration);
                Guid newSecret = Guid.NewGuid();
                _appSettingReposirtory.UpdateSecritKey(newSecret.ToString());
                if (dataBaseConfigrationAdd != null)
                    return "1";
            }
            
            return "0";
        }
        public async Task<SystemConfigration> UpdateSystemConfigration(SystemConfigration systemConfigration)
        {
            try
            {
                int[] listOfEnums = { (int)AppSettingGroupEnum.SystemType, (int)AppSettingGroupEnum.ConnectionType };
                List<ApplicationSetting> applicationSetting = new List<ApplicationSetting>() { new ApplicationSetting { Id = 1, GroupId = (int)AppSettingGroupEnum.SystemType, Key = "SystemType", Value = systemConfigration.SystemType },
                new ApplicationSetting { Id = 2, GroupId = (int)AppSettingGroupEnum.SystemType, Key = "ConnectionType", Value = systemConfigration.ConnectionType } };
                var appSetting = await _appSettingReposirtory.UpdateApplicationSettingsByGroupIdAsync(listOfEnums, applicationSetting);
                return await GetSystemAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public async Task<AppSettingUrl> UdateAppSettingUrl(AppSettingUrl appSettingUrl, AppSettingGroupEnum appSettingGroupEnum)
        {
            List<ApplicationSetting> applicationSetting = new List<ApplicationSetting>() {
                new ApplicationSetting { Id = 1009, GroupId = (int)appSettingGroupEnum, Key = "Url", Value = appSettingUrl.ApplictionUrl },
                new ApplicationSetting { Id = 1010, GroupId = (int)appSettingGroupEnum, Key = "Port", Value = appSettingUrl.ApplicationPort },
                new ApplicationSetting { Id = 3025, GroupId = (int)appSettingGroupEnum, Key = "UserName", Value = appSettingUrl.UserName },
                new ApplicationSetting { Id = 3026, GroupId = (int)appSettingGroupEnum, Key = "Password", Value = appSettingUrl.Password }
            };
            var appSetting = await _appSettingReposirtory.UpdateApplicationSettingsByGroupIdAsync((int)appSettingGroupEnum, applicationSetting);

            return new AppSettingUrl
            {
                ApplictionUrl = appSetting.FirstOrDefault(x => x.Key == "Url").Value,
                ApplicationPort = appSetting.FirstOrDefault(x => x.Key == "Port").Value,
                UserName = appSetting.FirstOrDefault(x => x.Key == "UserName").Value,
                Password = appSetting.FirstOrDefault(x => x.Key == "Password").Value
            };
        }
        public async Task<DataBaseConfigration> UdateDataBaseConfigration(DataBaseConfigration dataBaseConfigration, AppSettingGroupEnum appSettingGroupEnum)
        {
            List<ApplicationSetting> applicationSetting = new List<ApplicationSetting>()
            {
                new ApplicationSetting { Id=1025, GroupId = (int)AppSettingGroupEnum.DatabaseConfigration, Key = "ServerName", Value = dataBaseConfigration.ServerName },
                new ApplicationSetting {Id=1026, GroupId = (int)AppSettingGroupEnum.DatabaseConfigration, Key = "DataBaseName", Value = dataBaseConfigration.DataBaseName} ,
                new ApplicationSetting { Id=1027,GroupId = (int)AppSettingGroupEnum.DatabaseConfigration, Key = "UserName", Value = dataBaseConfigration.UserName},
                new ApplicationSetting {Id=1028, GroupId = (int)AppSettingGroupEnum.DatabaseConfigration, Key = "Password", Value = dataBaseConfigration.Password}
            };
            var appSetting = await _appSettingReposirtory.UpdateApplicationSettingsByGroupIdAsync((int)appSettingGroupEnum, applicationSetting);
            string serverName = appSetting.FirstOrDefault(x => x.Key == "ServerName").Value;
            string dataBaseName = appSetting.FirstOrDefault(x => x.Key == "DataBaseName").Value;
            string userName = appSetting.FirstOrDefault(x => x.Key == "UserName").Value;
            string password = appSetting.FirstOrDefault(x => x.Key == "Password").Value;

            return new DataBaseConfigration(serverName, dataBaseName, userName, password);
        }
        public async Task<AppSettingMail> UdateAppSettingMail(AppSettingMail appSettingMail)
        {
            var appSetting = new List<ApplicationSetting>();

            appSetting = (await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.MailConfigration)).ToList();
            if (appSetting.Count() != 0)
            {
                appSetting.FirstOrDefault(s => s.Key == "Host").Value = appSettingMail.Host;
                appSetting.FirstOrDefault(s => s.Key == "From").Value = appSettingMail.mailForm;
                appSetting.FirstOrDefault(s => s.Key == "Password").Value = appSettingMail.Password;
                appSetting.FirstOrDefault(s => s.Key == "Port").Value = appSettingMail.Port;

            }
            else
            {
                appSetting = new List<ApplicationSetting>() {
                  new ApplicationSetting { GroupId = (int)AppSettingGroupEnum.MailConfigration, Key = "Host", Value = appSettingMail.Host }
                , new ApplicationSetting {GroupId = (int)AppSettingGroupEnum.MailConfigration, Key = "From", Value = appSettingMail.mailForm }
                , new ApplicationSetting {GroupId = (int)AppSettingGroupEnum.MailConfigration, Key = "Password", Value = appSettingMail.mailForm }
                , new ApplicationSetting {GroupId = (int)AppSettingGroupEnum.MailConfigration, Key = "Port", Value = appSettingMail.Port }};
            }



            var appSettings = await _appSettingReposirtory.UpdateApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.MailConfigration, appSetting.ToList());

            AppSettingMail newappSettingMail = new AppSettingMail
            {
                Host = appSetting.FirstOrDefault(s => s.Key == "Host").Value,
                mailForm = appSetting.FirstOrDefault(s => s.Key == "From").Value,
                Password = appSetting.FirstOrDefault(s => s.Key == "Password").Value,
                Port = appSetting.FirstOrDefault(s => s.Key == "Port").Value,
            };

            return appSettingMail;
        }
        #endregion
    }
}
