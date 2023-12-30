using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Common.AspNetCore.Autorizetion.DynamicPermissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Common.AspNetCore.Autorizetion.DynamicAuthorizationService;

public class MvcActionsDiscoveryService : IMvcActionsDiscoveryService
{
    private readonly ConcurrentDictionary<string, Lazy<ICollection<ControllerViewModel>>> _allSecuredActionsWithPloicy = new ConcurrentDictionary<string, Lazy<ICollection<ControllerViewModel>>>();

    public ICollection<ControllerViewModel> MvcControllers { get; }


    public MvcActionsDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
    {
        if (actionDescriptorCollectionProvider == null)
        {
            throw new ArgumentNullException(nameof(actionDescriptorCollectionProvider));
        }

        MvcControllers = new List<ControllerViewModel>();
        var LastControllerName = string.Empty;
        ControllerViewModel currentController = null;

        var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
        foreach (var actionDescriptor in actionDescriptors)
        {
            if (!(actionDescriptor is ControllerActionDescriptor descriptor))
            {
                continue;
            }

            var ControllerTypeInfo = descriptor.ControllerTypeInfo;
            var ActionMethodInfo = descriptor.MethodInfo;

            if (LastControllerName != descriptor.ControllerName)
            {
                currentController = new ControllerViewModel
                {
                    AreaName = ControllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                    ControllerAttributes = GetAttributes(ControllerTypeInfo),
                    ControllerDisplayName = ControllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    ControllerName = descriptor.ControllerName,
                };
                MvcControllers.Add(currentController);

                LastControllerName = descriptor.ControllerName;
            }

            currentController?.MvcActions.Add(new ActionViewModel
            {
                ControllerId = currentController.ControllerId,
                ActionName = descriptor.ActionName,
                ActionDisplayName = ActionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                ActionAttributes = GetAttributes(ActionMethodInfo),
                IsSecuredAction = IsSecuredAction(ControllerTypeInfo, ActionMethodInfo)
            });
        }
    }

    public async Task<ICollection<ControllerViewModel>> GetAllSecuredControllerActionsWithPolicy(string policyName)
    {
        var Getter = _allSecuredActionsWithPloicy.GetOrAdd(policyName, y => new Lazy<ICollection<ControllerViewModel>>(
            () =>
            {
                var Controllers = new List<ControllerViewModel>(MvcControllers);
                foreach (var controller in Controllers)
                {
                    controller.MvcActions = controller.MvcActions.Where(
                        model => model.IsSecuredAction &&
                        (
                        model.ActionAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName ||
                        controller.ControllerAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName
                        )).ToList();
                }
                return Controllers.Where(model => model.MvcActions.Any()).ToList();
            }));
        await Task.CompletedTask;
        return Getter.Value;
    }

    private static List<Attribute> GetAttributes(MemberInfo actionMethodInfo)
    {
        return actionMethodInfo.GetCustomAttributes(inherit: true)
                               .Where(attribute =>
                               {
                                   var AttributeNamespace = attribute.GetType().Namespace;
                                   return AttributeNamespace != typeof(CompilerGeneratedAttribute).Namespace &&
                                          AttributeNamespace != typeof(DebuggerStepThroughAttribute).Namespace;
                               })
                                .Cast<Attribute>()
                               .ToList();
    }

    private static bool IsSecuredAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
    {
        var ActionHasAllowAnonymousAttribute = actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(inherit: true) != null;
        if (ActionHasAllowAnonymousAttribute)
        {
            return false;
        }

        var ControllerHasAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
        if (ControllerHasAuthorizeAttribute)
        {
            return true;
        }

        var ActionMethodHasAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
        if (ActionMethodHasAuthorizeAttribute)
        {
            return true;
        }

        return false;
    }

}