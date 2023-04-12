using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Users;
using BSMediator.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NFC.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFC.Data.Repository
{
    public class UserApiRepository : IUserRepository
    {
        private readonly HttpClientService _httpClient;

        public UserApiRepository(HttpClientService httpClientService)
        {
            _httpClient = httpClientService;
        }

        #region Add
     
        #endregion

        #region Update
       
        #endregion

        #region Get
        public async Task<EmplyeeList> GetUsers(string emp_code, string first_name, string last_name, string department, string app_status, string token,string pageNo)
        {
            string url = Config.AppUrl+AppEndPoint.EmployeEndPoint + "?emp_code=" + emp_code + "&page=" + pageNo + "&first_name=" + first_name + "&last_name=" + last_name + "&department=" + department + "&app_status=" + app_status + "&limit=100";
            var result = await  _httpClient.CallService(url, token);
            if (result.ReturnData == null)
                return null;
            var json = (string)result.ReturnData;
            EmplyeeList emplyeeList = JsonConvert.DeserializeObject<EmplyeeList>((string)result.ReturnData);

            return emplyeeList;
        }
        public async Task<Datum> GetUserById(int personId, string token)
        {
            var url = Config.AppUrl + AppEndPoint.EmployeEndPoint + personId + "/";
            var result = await _httpClient.CallService(url, token);
            var json = (string)result.ReturnData;
            Datum emplyeeList = JsonConvert.DeserializeObject<Datum>((string)result.ReturnData);

            return emplyeeList;
        }
        #endregion

        #region Update
        #endregion

        #region Commit
       
        #endregion
    }
}

