using AutoMapper;
using BSMediator.Core.Entities;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreater;
using BSMediator.Core.Repositories.Opreations;
using BSMediator.Core.Services.Opreater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.Opreater
{
    public class ActionService: IActionService
    {
        private readonly IActionRepository _actionRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public ActionService(IActionRepository actionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        #region Get
        public Task<IEnumerable<BSMediator.Core.Entities.Action>> GetActionAsync()
        {
            return _actionRepository.GetActionAsync();
        }
        public async Task<IEnumerable<UserActionWithActionNameModel>> GetUserActionAsync(int userId)
        {
            var action = await _actionRepository.GetActionAsync();
            var userAction = await _actionRepository.GetUserActionAsync(userId);
            var actionNotIn = action.Where(a => !userAction.Select(s => s.Id).Contains(a.Id));
            List<UserActionWithActionNameModel> userActionWithActionNameModels = new List<UserActionWithActionNameModel>();
            var userActions = userAction.ToList();
            userActions.AddRange(actionNotIn.Select(s => new UserAction
            {
                ActionId = s.Id,
                UserId = userId,
                IsActive = false,
                Action = s,
            }));

            userActionWithActionNameModels.AddRange(userActions.Select(s => new UserActionWithActionNameModel
            {
                Id = s.Id,
                ActionName=s.Action.ActionName,
                ActionId=s.ActionId,
                IsActive=s.IsActive,
                UserId=userId,
            }));
            return userActionWithActionNameModels;
        }
        public bool CheckUserActionIsActive(int userId,int[] actionId)
        {
            return _actionRepository.CheckUserActionIsActive(userId, actionId);
        }

        #endregion

        #region AddUpdate
        public async Task<BSMediator.Core.Entities.Action> AddActionAsync(BSMediator.Core.Entities.Action action)
        {
            return await _actionRepository.AddActionAsync(action);
        }
        public async Task<IEnumerable<BSMediator.Core.Entities.UserAction>> AddUpdateUserActionAsync(List<UserActionModel> userActionModels, int userId)
        {
            var userActions = _mapper.Map<List<UserAction>>(userActionModels);
            if (!userActions.All(ua => ua.UserId == userId))
                return null;
            var IsUserExists = await _userRepository.CheckUserById(userId);
            if (!IsUserExists)
                return null;
            foreach (var item in userActions)
            {
                var isExistAction = await _actionRepository.CheckActionByidAsync(item.ActionId);
                if (!isExistAction)
                    return null;
            }
            return await _actionRepository.AddUpdateUserActionAsync(userActions, userId);

        }
        #endregion

    }
}
