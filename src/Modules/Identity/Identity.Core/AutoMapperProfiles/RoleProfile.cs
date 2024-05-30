using AutoMapper;
using Identity.Core.Dto.Role;
using Identity.Data.Entities;
using Identity.ViewModels.RoleManager;

namespace Identity.Core.AutoMapperProfiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap< RoleDto, Role > ()
            .ForMember(x=>x.Claims,opt=>opt.MapFrom(x=>x.Claims))
            .ReverseMap();
        CreateMap<Role, RoleVm>()
            .ForMember(x=>x.RoleId,opt=>opt.MapFrom(x=>x.Id))
            .ReverseMap();

        CreateMap<Role, GetUserRoleDto>()
            .ForMember(x => x.RoleId, v => v.MapFrom(x => x.Id))
            .ForMember(x => x.RoleName, s => s.MapFrom(x => x.Name));
        CreateMap<Role, GetUserRoleViewModel>()
            .ForMember(x => x.RoleId, v => v.MapFrom(x => x.Id))
            .ForMember(x => x.RoleName, s => s.MapFrom(x => x.Name));
    }
}
