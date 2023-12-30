using System.Security.Claims;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Microsoft.AspNetCore.Http;

namespace Common.AspNetCore.Autorizetion.DynamicAuthorizationService;

public class SecurityTrimmingService : ISecurityTrimmingService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;

    public SecurityTrimmingService(
        IHttpContextAccessor httpContextAccessor,
        IMvcActionsDiscoveryService mvcActionsDiscoveryService)
    {
        _httpContextAccessor = httpContextAccessor;
        _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
    }

    public bool CanCurrentUserAccess(string? area, string? controller, string? action)
    {
        return _httpContextAccessor.HttpContext != null && CanUserAccess(_httpContextAccessor.HttpContext.User, area, controller, action);
    }

    public bool CanUserAccess(ClaimsPrincipal user, string? area, string? controller, string? action)
    {
        var currentClaimValue = $"{area}:{controller}:{action}";
        var securedControllerActions = _mvcActionsDiscoveryService
            .GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission).Result;
        if (securedControllerActions.SelectMany(x => x.MvcActions).All(x => x.ActionId != currentClaimValue))
        {
            throw new KeyNotFoundException($@"The `secured` area={area}/controller={controller}/action={action} with
`ConstantPolicies.DynamicPermission` policy not found.
Please check you have entered the area/controller/action names
correctly and also it's decorated with the correct security policy.");
        }

        if (user.Identity != null && !user.Identity.IsAuthenticated)
        {
            return false;
        }

        return user.HasClaim(claim => claim.Type == ConstantPolicies.DynamicPermissionClaimType &&
                                      claim.Value == currentClaimValue);
    }

}