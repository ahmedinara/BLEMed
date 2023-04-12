using BSMediator.Core.BioTimeEntites;
using BSMediator.Core.Enums;
using BSMediator.Core.Repositories.Users;
using BSMediator.Core.Shared;
using BSMediator.Data.Repositories.User;
using NFC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.User
{
    public class UserSwitcher
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpClientService _httpClient;
        private int _connType;
        private readonly BioTimeContext _context;
        public UserSwitcher(int conntype, HttpClientService httpClientService, BioTimeContext bioTimeContext)
        {
            _connType=conntype;
            _httpClient=httpClientService;
            _context=bioTimeContext;
        }

        public static IUserRepository CreateUserInstance(int connType, HttpClientService httpClientService, BioTimeContext bioTimeContext)
        {
            if (connType == (int)ConnTypeEnum.Api)
            {
                return new UserApiRepository(httpClientService);
            }
            else
                return new UserDbRepository(bioTimeContext);
        }
    }
}
