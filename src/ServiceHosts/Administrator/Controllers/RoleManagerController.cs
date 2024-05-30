using Administrator.Infrastructure.DataTableHelper;
using Administrator.Infrastructure.Routeing;
using Common.Application;
using Common.Application.DataTableConfig;
using Common.AspNetCore;
using Common.AspNetCore.Notification;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.Core.Services;
using Identity.ViewModels.RoleManager;
using Microsoft.AspNetCore.Mvc;
using TicketModule.ViewModel;

namespace Administrator.Controllers
{
    //[BaseAuthorize(AssignableToRolePermissions.CanManageRole)]
    public class RoleManagerController : BaseController
    {
        #region ctur

        private readonly IRoleService _roleService;
        private readonly INotification _notification;

        public RoleManagerController(IRoleService roleService, INotification notification)
        {
            _roleService = roleService;
            _notification = notification;
        }

        #endregion

        public IActionResult Index()
        {
            ActiveMenu("RoleManager");
            return View(InitTableView.RoleDataTable());
        }

        #region Crud

        [HttpGet, Route(BaseRouteing.RouteDefaultOptinalRoleId)]
        public async Task<IActionResult> RenderRole(Guid? roleId)
        {
            var roleViewModel = new RoleVm();
            if (roleId.HasValue)
            {
                var result = await _roleService.GetRoleViewModel(new RequestQueryById(roleId.Value));
                if (result.IsSuccessed && result.Data != null)
                {
                    roleViewModel = result.Data;
                }
            }

            return PartialView("_RenderCreateUpdate", roleViewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(RoleVm model)
        {
            if (ModelState.IsValid)
            {
                if (model.RoleId != null)
                {
                    await _roleService.UpdateRole(new RoleDto(model.RoleId.Value, model.Name, model.Description));
                }
                else
                {
                    await _roleService.CreateRole(new RequestCreateRoleDto(model.Name, model.Description));
                }
            }

            SetAjaxNotification(OperationMessages.OperationSuccess);
            return PartialView("_RenderCreateUpdate", model);
        }

        [Route(BaseRouteing.RouteDefaultRoleId), HttpGet]
        public async Task<IActionResult> RenderRemove(Guid roleId)
        {
            var findRole = await _roleService.GetRoleForRemove(new RequestQueryById(roleId));
            return PartialView("_Delete", findRole.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(TicketViewModel model)
        {
            ModelState.Remove("Title");
            if (!ModelState.IsValid) return PartialView("_Delete", model);

            var result = await _roleService.DeleteRole(new RequestQueryById(model.Id.Value));
            if (result.IsSuccessed)
                TempData["notification"] = OperationMessages.OperationSuccess;
            return PartialView("_Delete", model);
        }

        [HttpGet, Route(BaseRouteing.RouteDefaultRoleId)]
        public async Task<IActionResult> ManageRole(Guid roleId)
        {
            ActiveMenu("RoleManager");
            if (string.IsNullOrWhiteSpace(roleId.ToString())) return NotFound(roleId);
            var getRoleWithPermission =
                await _roleService.GetRoleWithPermissionsViewModel(new RequestQueryById(roleId));
            getRoleWithPermission.Data ??= new GetRoleWithPermissionsViewModel();
            _roleService.GetPermissionViewModel(getRoleWithPermission.Data);
            return View(getRoleWithPermission.Data);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRole(GetRoleWithPermissionsViewModel model)
        {
            ActiveMenu("RoleManager");
            ModelState.Remove("Permissions");
            if (!ModelState.IsValid) return View(model);
            _roleService.GetPermissionViewModel(model);
            var result = await _roleService.UpdateRolePermission(model);
            if (result.IsSuccessed)
            {
                _notification.SuccessNotify(OperationMessages.OperationSuccess, OperationMessageTitleResult.موفقیت_آمیز);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }

        #endregion

        #region Pagging

        [HttpPost]
        public async Task<IActionResult> GetListPaging()
        {
            Request.GetDataFromRequest(out FiltersFromRequestDataTableBase filtersFromRequest);
            var result = await _roleService.GetPaggingViewModel(filtersFromRequest);
            return new JsonResult(result);
        }

        #endregion
    }
}