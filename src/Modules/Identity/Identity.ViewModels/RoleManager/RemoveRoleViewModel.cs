namespace Identity.ViewModels.RoleManager;

public class RemoveRoleViewModel
{
    public RemoveRoleViewModel(Guid roleId, string roleName)
    {
        RoleId = roleId;
        RoleName = roleName;
    }

    public RemoveRoleViewModel()
    {
        
    }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
}