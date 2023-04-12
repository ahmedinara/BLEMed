using BSMediator.Core.Repositories.Opreations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSMediator.Core.Entities;
using BSMediator.Core.Services.Opreater;
using BSMediator.Core.Services.Opreation;
using AutoMapper;
using BSMediator.Core.Models;

namespace BSMediator.Service.Services.Opreater
{
    public class UserService: IUserService
    {
        private readonly IAuthAdminService _authService;

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,IMapper mapper,IAuthAdminService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _mapper = mapper;
        }

        #region Add
        public async Task<BSMediator.Core.Entities.User> AddUser(BSMediator.Core.Entities.User user)
        {
            user.Password = _authService.CreateHashSaltPassword(user.Password, out byte[] salt);
            user.PasswordSalt = salt;
            user.CreatedOn = DateTime.Now;
            var entity = await _userRepository.AddUser(user);
            return entity;
        }
        #endregion

        #region Update
        public async Task<BSMediator.Core.Entities.User> UpdateUser(UserModel userModel, int userId, int updatedUser)
        {
            var user = _mapper.Map<BSMediator.Core.Entities.User>(userModel);
            var userExists = await _userRepository.GetUserById(user.Id);
            if (userExists == null)
                return null;
            user.Password = userExists.Password;
            user.PasswordSalt = userExists.PasswordSalt;
            user.CreatedOn = userExists.CreatedOn;
            user.UpdateBy = updatedUser;
            user.UpdatedOn = DateTime.Now;
            var entity = await _userRepository.UpdateUser(user);
            return entity;
        }
        #endregion

        #region Get
        public async Task<UserModel> GetUserModelById(int id)
        {
            return _mapper.Map<UserModel>(await _userRepository.GetUserById(id));
        }
        public async Task<IEnumerable<BSMediator.Core.Entities.User>> GetAll()
        {
            return await _userRepository.GetUsers();
        }
        public async Task<IEnumerable<UserDLL>> GetDLL()
        {
            return _mapper.Map<IEnumerable<UserDLL>>(await _userRepository.GetUsers());
        }
        public async Task<BSMediator.Core.Entities.User> GetById(int id)
        {
            return await _userRepository.GetUserById(id);
        }
        #endregion

    }
}
