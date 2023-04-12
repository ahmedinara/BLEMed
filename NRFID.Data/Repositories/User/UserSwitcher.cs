using BSMediator.Core.BioTimeEntites;
using BSMediator.Core.Enums;
using BSMediator.Core.Repositories.Users;
using BSMediator.Core.Shared;
using NFC.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Data.Repositories.User
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

        public IUserRepository CreateUserInstance()
        {
            if (_connType == (int)ConnTypeEnum.Api)
            {
                return new UserApiRepository(_httpClient);
            }
            else
                return new UserDbRepository(_context);
        }
    }
}
