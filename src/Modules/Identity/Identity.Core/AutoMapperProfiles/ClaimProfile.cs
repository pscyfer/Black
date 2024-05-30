using AutoMapper;
using Identity.Core.Dto.Claim;
using Identity.Data.Entities;

namespace Identity.Core.AutoMapperProfiles;

public class ClaimProfile:Profile
{
    public ClaimProfile()
    {
        CreateMap<RoleClaim, RoleClaimDto>()
            .ReverseMap();
        CreateMap<RoleClaim, AddRoleClaimDto>()
            .ReverseMap();
    }
}