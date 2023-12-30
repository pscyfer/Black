using System.Xml.Linq;

namespace Identity.Core.Security;
public class PermissionService : IPermissionService
{
    #region Fields
    private const string PermissionsElement = "Permissions";
    private const string PermissionElement = "Permission";
    #endregion

    #region Ctor
    public PermissionService()
    {

    }
    #endregion


    public IEnumerable<string> GetDescriptions(XElement permissionsAsXml)
    {
        var permissions = AssignableToRolePermissions.Permissions;
        return permissions.Where(
            r =>
                GetUserPermissionsAsList(permissionsAsXml)
                    .ToArray()
                    .Any(p => p == r.Name)).Select(r => r.Description);
    }

    public XElement GetPermissionsAsXml(params string[] permissionNames)
    {
        var permissionsAsXml = new XElement(PermissionsElement);
        foreach (var permissionName in permissionNames)
        {
            permissionsAsXml.Add(new XElement(PermissionElement, permissionName));
        }
        return permissionsAsXml;
    }

    public List<string> GetUserPermissionsAsList(XElement permissionsAsXml)
    {
        return permissionsAsXml.Elements(PermissionElement).Select(a => a.Value).ToList();
    }

    public List<string> GetUserPermissionsAsList(List<XElement> permissionsAsXmls)
    {
        var permissions = new List<string>();
        foreach (var permissionsAsXml in 
            permissionsAsXmls.Where(permissionsAsXml => permissionsAsXml != null))
        {
            permissions.AddRange(permissionsAsXml.Elements(PermissionElement)
                .Select(a => a.Value).ToList());
        }
        return permissions;
    }
}
