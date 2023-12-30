namespace Identity.Core.Security;
public class PermissionRecord
{
    public string RoleName { get; set; }
    public IEnumerable<PermissionModel> Permissions { get; set; }
}
