using System.Xml.Linq;

namespace Identity.Core.Security;
public interface IPermissionService
{
    XElement GetPermissionsAsXml(params string[] permissionNames);
    List<string> GetUserPermissionsAsList(XElement permissionsAsXml);
    List<string> GetUserPermissionsAsList(List<XElement> permissionsAsXmls);
    IEnumerable<string> GetDescriptions(XElement permissionsAsXml);

}
