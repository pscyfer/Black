using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Common.AspNetCore.Autorizetion.DynamicAuthorizationService;

public class DynamicPermissionRequirement : IAuthorizationRequirement
{
}

public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
{
    private readonly ISecurityTrimmingService _securityTrimmingService;

    public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService)
    {
        _securityTrimmingService = securityTrimmingService;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DynamicPermissionRequirement requirement)
    {

        var MVCContext = context.Resource as HttpContext;
        if (MVCContext == null)
        {
            return Task.CompletedTask;
        }
        var ActionDescriptor = MVCContext.GetRouteData();

        ActionDescriptor.Values.TryGetValue("area", out var areaName);
        var Area = string.IsNullOrWhiteSpace((string?)areaName) ? string.Empty : areaName;

        ActionDescriptor.Values.TryGetValue("controller", out var controllerName);
        var Controller = string.IsNullOrWhiteSpace((string?)controllerName) ? string.Empty : controllerName;

        ActionDescriptor.Values.TryGetValue("action", out var actionName);
        var Action = string.IsNullOrWhiteSpace((string?)actionName) ? string.Empty : actionName;

        if (_securityTrimmingService.CanCurrentUserAccess(Area.ToString(), Controller.ToString(), Action.ToString()))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}