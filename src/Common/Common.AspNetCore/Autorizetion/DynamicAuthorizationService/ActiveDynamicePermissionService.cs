using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Common.AspNetCore.Autorizetion.DynamicAuthorizationService;

public static class ActiveDynamicePermissionService
{
    public static IServiceCollection AddDynamicPermissionService(this IServiceCollection service)
    {
        service.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
        service.AddHttpContextAccessor();
        service.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
        service.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();


        return service;
    }
}