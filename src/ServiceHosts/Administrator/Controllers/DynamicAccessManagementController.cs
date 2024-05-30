using Administrator.Infrastructure.Routeing;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Common.AspNetCore.Notification;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Common.Application;

namespace Administrator.Controllers
{
    [DisplayName("مدیریت مجوز ها")]

    public class DynamicAccessManagementController : BaseController
    {

        private readonly IRoleService _roleService;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;
        private readonly INotification _notification;

        public DynamicAccessManagementController(IRoleService roleService, IMvcActionsDiscoveryService mvcActionsDiscovery, INotification notification)
        {
            _roleService = roleService;
            _mvcActionsDiscovery = mvcActionsDiscovery;
            _notification = notification;
        }

        [HttpGet, Route(BaseRouteing.DynamicAccessManagementRoleId)]
        public async Task<IActionResult> Index(RequestQueryById roleId)
        {
            if (!string.IsNullOrWhiteSpace(roleId.ToString()))
            {
                var roles = await _roleService.FindClaimsInRole(roleId);
                if (roles is null)
                    return NotFound();

                var securedControllerAction = await
                    _mvcActionsDiscovery.
                        GetAllSecuredControllerActionsWithPolicy(
                            ConstantPolicies.DynamicPermission);
                return View(new DynamicAccessDto()
                {
                    RoleIncludeRoleClaim = roles,
                    SecuredControllerActions = securedControllerAction
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(DynamicAccessDto model)
        {
            var result = await _roleService.AddOrUpdateRoleClaimAsync(new RequestQueryById(model.RoleId), ConstantPolicies.DynamicPermissionClaimType, model.ActionIds);
            if (!result.IsSuccessed)
            {
                _notification.Notify("در حین انجام عملیات خطایی رخ داده است", OperationMessageTitleResult.خطاا, NotificationType.error);
            }
            else
                _notification.Notify("سطح دسترسی با موفقیت  بروزرسانی شد", OperationMessageTitleResult.موفقیت_آمیز, NotificationType.success);
            return RedirectToAction("Index", "RoleManager");

        }

    }
}
