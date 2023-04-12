using AutoMapper;
using BSMediator.Core.BioTimeEntites;
using BSMediator.Core.Entities;
using BSMediator.Core.Models;
using System;
using System.Linq;

namespace IMS.Core.MappProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<User, UserDLL>()
             .ForMember(mod => mod.FullName, opt => opt.MapFrom(ent => ent.FristName+" "+ent.LastName))
                .ReverseMap();

            this.CreateMap<User, UserModel>()
             .ReverseMap();

            this.CreateMap<CommRole, CommRoleModel>()
                .ForMember(mod => mod.commRoleDetials, opt => opt.MapFrom(ent => ent.commRoleDetials))
                .ReverseMap();
            this.CreateMap<CommRoleDetials, CommRoleDetialsModel>()
               .ForMember(mod => mod.TypeValue, opt => opt.MapFrom(ent => ent.ApplicationSetting.Value))
                .ReverseMap();
            this.CreateMap<Datum, PersonnelEmployee>()
                .ForMember(mod => mod.Id, opt => opt.MapFrom(ent => ent.id))
                .ForMember(mod => mod.FirstName, opt => opt.MapFrom(ent => ent.first_name))
                .ForMember(mod => mod.LastName, opt => opt.MapFrom(ent => ent.last_name))
                .ForMember(mod => mod.CardNo, opt => opt.MapFrom(ent => ent.card_no))
                .ForMember(mod => mod.Mobile, opt => opt.MapFrom(ent => ent.mobile))
                .ForMember(mod => mod.EmpCode, opt => opt.MapFrom(ent => ent.emp_code))
                .ReverseMap();
            this.CreateMap<UserAction, UserActionModel>().ReverseMap();

        }
    }
}
