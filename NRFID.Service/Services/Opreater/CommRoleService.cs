using AutoMapper;
using BSMediator.Core.Entities;
using BSMediator.Core.Enums;
using BSMediator.Core.Models;
using BSMediator.Core.Repositories.Opreater;
using BSMediator.Core.Repositories.Opreation;
using BSMediator.Core.Services.Opreater;
using NFC.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSMediator.Service.Services.Opreater
{
    public class CommRoleService : ICommRoleService
    {
        private readonly ICommRoleRepository _commRoleRepository;
        private readonly IAppSettingRepository _appSettingReposirtory;
        private readonly IMapper _mapper;

        public CommRoleService(ICommRoleRepository commRoleRepository, IAppSettingRepository appSettingRepository,IMapper mapper)
        {
            _commRoleRepository = commRoleRepository;
            _appSettingReposirtory = appSettingRepository;
            _mapper = mapper;
        }

        #region Get
        public async Task<CommRoleParent> GetCommRole()
        {
            var commRoles  =await _commRoleRepository.GetCommRoleWithDetialsAsync();
            var appSetting = await _appSettingReposirtory.GetApplicationSettingsByGroupIdAsync((int)AppSettingGroupEnum.ConnType);
            var commRoleModels =_mapper.Map<List<CommRoleModel>>(commRoles);
            
            foreach (var commRoleModel in commRoleModels)
            {
                var commRole = commRoles.FirstOrDefault(f => f.Id == commRoleModel.Id);
                var appSettings = appSetting.Where(a => !commRole.commRoleDetials.Select(x => x.TypeId).Contains(a.Id));
                foreach (var applicationSetting in appSettings)
                {
                    commRoleModel.commRoleDetials.Add(new CommRoleDetialsModel { TypeValue = applicationSetting.Value,TypeId= applicationSetting.Id });
                }
                commRoleModel.commRoleDetials.OrderBy(x => x.TypeValue);
            }
            return new CommRoleParent { CommRoleModels=commRoleModels, ConnectionOptions = appSetting.Select(s => new ConnectionOption {Name=s.Value }).OrderBy(x => x.Name).ToList()};
        }
        #endregion

        #region Add
        public async Task<string> AddCommRole(List<CommRoleModel> commRoleModels)
        {
            var commRoles = _mapper.Map<List<CommRole>>(commRoleModels);
            var result = await _commRoleRepository.AddUpdateCommRoleWithDetialsAsync(commRoles);
            if(result == null)
            return "0";
            return "1";
        }
        #endregion
    }
}
