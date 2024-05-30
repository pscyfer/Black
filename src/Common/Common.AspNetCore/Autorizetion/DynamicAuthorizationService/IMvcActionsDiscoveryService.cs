using Common.AspNetCore.Autorizetion.DynamicPermissions;

namespace Common.AspNetCore.Autorizetion.DynamicAuthorizationService;

public interface IMvcActionsDiscoveryService
{
    Task<ICollection<ControllerViewModel>> GetAllSecuredControllerActionsWithPolicy(string policyName);
}