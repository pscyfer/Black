using System.Security.Claims;
using Identity.Core.Security;
using System.Security.Principal;

namespace Identity.Core;

public static class AuthHelper
{
    public static string GetUserId(IPrincipal user)
    {
        if (user is not { Identity.IsAuthenticated: true }) return "";
        var userTypeClaim = ((ClaimsPrincipal)user);
        var getValue = userTypeClaim.Claims.FirstOrDefault(x => x.Type == "UserId");
        if (getValue != null)
            return getValue.Value;
        return "";
    }
    public static bool IsUserAuthenticated(IPrincipal? user)
    {
        if (user == null)
            return false;
        return user.Identity is { IsAuthenticated: true };
    }

    public static string GetFullName(IPrincipal? user)
    {
        if (user is { Identity.IsAuthenticated: true })
        {
            var userTypeClaim = ((CustomClaimsPrincipal)user);
            var getValue = userTypeClaim.Claims.FirstOrDefault(x => x.Type == "FullName");
            if (getValue != null)
                return getValue.Value;
        }
        return "";
    }

    public static string GetRoleNames(IPrincipal user)
    {
        if (user is { Identity.IsAuthenticated: true })
        {
            var userTypeClaim = ((CustomClaimsPrincipal)user);
            var getValue = userTypeClaim.Claims.FirstOrDefault(x => x.Type == "RoleName");
            if (getValue != null)
                return getValue.Value;
        }
        return "";
    }

    public static string GetUserAvatar(IPrincipal user)
    {

        var userTypeClaim = ((CustomClaimsPrincipal)user);
        var avatarClaim = userTypeClaim.Claims.FirstOrDefault(x => x.Type == "Avatar");
        return avatarClaim != null ? avatarClaim.Value : "";
    }

}