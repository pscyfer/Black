using Common.AspNetCore.Autorizetion.DynamicPermissions;

namespace Identity.Core.Dto.Role;

public class DynamicAccessDto
{
    public string[]? ActionIds { get; set; }
    public Guid RoleId { get; set; }
    public RoleDto RoleIncludeRoleClaim { get; set; }
    public ICollection<ControllerViewModel> SecuredControllerActions { get; set; }
}