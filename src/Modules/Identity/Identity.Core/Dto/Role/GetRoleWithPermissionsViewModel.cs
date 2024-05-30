using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Core.Dto.Role;

public class GetRoleWithPermissionsDto
{
    public GetRoleWithPermissionsDto()
    {
        PermissionNames = Array.Empty<string>();
    }

    public GetRoleWithPermissionsDto(Guid roleId, string name)
    {
        RoleId = roleId;
        Name = name;
         PermissionNames = Array.Empty<string>();
    }

    public Guid RoleId { get; set; }
    public string Name { get; set; }
    public string[] PermissionNames { get; set; }

    /// <summary>
    /// لیست دسترسی ها به صورت لیست آبشاری
    /// </summary>
    public IEnumerable<SelectListItem> Permissions { get; set; }
}