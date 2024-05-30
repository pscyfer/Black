using System.ComponentModel.DataAnnotations;
using Identity.Core.Dto.Claim;
using Identity.ViewModels;

namespace Identity.Core.Dto.Role;

public class RoleDto
{
    public RoleDto(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public RoleDto()
    {
        
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<AddRoleClaimDto> Claims { get; set; }
}
