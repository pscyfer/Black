using Administrator.Infrastructure.DataTableHelper;
using Administrator.Infrastructure.Routeing;
using AutoMapper;
using Common.Application.Consts;
using Common.Application.DataTableConfig;
using Common.AspNetCore;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Common.AspNetCore.LocalFileProvider;
using Common.AspNetCore.Notification;
using Identity.Core;
using Identity.Core.Dto.Shared;
using Identity.Core.Dto.User;
using Identity.Core.Services;
using Identity.ViewModels.UserManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Common.Application;

namespace Administrator.Controllers
{
    [DisplayName("مدیریت کاربران")]
    [Authorize(Policy = ConstantPolicies.RequireAdministratorRole)]
    public class UserManagerController : BaseController
    {
        #region Field

        private readonly IUserService _userService;
        private readonly IFileManager _fileManager;
        private readonly IMapper _mapper;
        private readonly INotification _notification;
        #endregion
        #region Constructor

        public UserManagerController(IUserService userService, IFileManager fileManager, IMapper mapper, INotification notification)
        {
            _userService = userService;
            _fileManager = fileManager;
            _mapper = mapper;
            _notification = notification;
        }

        #endregion
        [DisplayName("مشاهده"), /*Authorize(Policy = ConstantPolicies.RequireAdministratorRole)*/]
        public IActionResult Index(CancellationToken cancellationToken)
        {
            ActiveMenu("UserManager");
            var viewModel = new UserManagerIndexVm();
            if (!cancellationToken.IsCancellationRequested)
            {
                viewModel = new UserManagerIndexVm(_userService.GetUserCountBy(true, cancellationToken).Result.Data,
                    _userService.GetUserCountBy(false, cancellationToken).Result.Data, InitTableView.UserDataTable(),
                    _userService.GetAllUserCount(cancellationToken).Result.Data,
                    _userService.GetAllNotConfirmAccountCount(cancellationToken).Result.Data);
            }

            return View(viewModel);
        }

        #region CRUD
        [HttpGet]

        public IActionResult RenderCreate()
        {
            return PartialView("_RenderCreateUser");
        }
        [DisplayName("افزودن"),/* Authorize(Policy = ConstantPolicies.DynamicPermission)*/]

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestCreateUserViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_RenderCreateUser");

            var result = await _userService.CreateUserAsync(model);
            if (!result.IsSuccessed) ModelState.AddModelError(string.Empty, result.Message);
            else
                SetAjaxNotification(OperationMessages.OperationSuccess);
            return PartialView("_RenderCreateUser");
        }

        [Route(BaseRouteing.RouteDefaultUserId), HttpGet]
        [DisplayName("مشاهده ویرایش"), Authorize(Policy = ConstantPolicies.DynamicPermission)]

        public async Task<IActionResult> Edit(Guid userId, CancellationToken cancellationToken)
        {
            ActiveMenu("UserManager");
            if (string.IsNullOrWhiteSpace(userId.ToString())) return RedirectToAction(nameof(Index));
            if (cancellationToken.IsCancellationRequested) return RedirectToAction(nameof(Index));
            var getUserForEdit = await _userService.GetUserForEdit(new RequestQueryById(userId));
            if (!getUserForEdit.IsSuccessed) return RedirectToAction(nameof(Index));
            var mapped = _mapper.Map<UserEditViewModel>(getUserForEdit.Data);
            return View(mapped);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [DisplayName("ویرایش"), Authorize(Policy = ConstantPolicies.DynamicPermission)]

        public async Task<IActionResult> Edit(UserEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            string filePath = viewModel.Avatar;
            if (viewModel.UserAvatarFile is not null)
                filePath = await _fileManager.CreateFileAtPathAsync(IdentityModulesDirectory.Avatar, viewModel.Avatar, viewModel.UserAvatarFile);
            var result = await _userService.EditUserAsync(new UserEditViewModel(
              viewModel.UserId, viewModel.UserName, viewModel.FirstName, viewModel.LastName, viewModel.Email,
              viewModel.PhoneNumber, viewModel.Gender, filePath, viewModel.IsActive, viewModel.BirthDay
              ));
            if (!result.IsSuccessed)
            {
                _notification.ErrorNotify(result.Message);
                return View(viewModel);
            }
            _notification.SuccessNotify(OperationMessages.OperationSuccess);
            return RedirectToAction("Index", "UserManager");
        }
        [Route(BaseRouteing.RouteDefaultUserId), HttpGet]
        public async Task<IActionResult> RenderRemove(Guid userId)
        {
            var viewModel = new RemoveUserViewModel();
            if (string.IsNullOrWhiteSpace(userId.ToString())) return PartialView("_Delete", viewModel);
            var findUser = await _userService
                .GetUserForRemove(new RequestQueryById(userId));
            if (!findUser.IsSuccessed) return PartialView("_Delete", viewModel);
            viewModel.UserId = findUser.Data.UserId;
            viewModel.FullName = findUser.Data.FullName;
            return PartialView("_Delete", viewModel);

        }

        [HttpPost, ValidateAntiForgeryToken]
        [DisplayName("حذف نهایی"), Authorize(Policy = ConstantPolicies.DynamicPermission)]

        public async Task<IActionResult> ConfirmDelete(RemoveUserViewModel model)
        {
            ModelState.Remove("FullName");
            if (!ModelState.IsValid) return PartialView("_Delete", model);

            var result = await _userService.RemoveUser(new RequestQueryById(model.UserId));
            if (result.IsSuccessed)
                TempData["notification"] = OperationMessages.OperationSuccess;
            return PartialView("_Delete", model);
        }

        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        [DisplayName("مشاهده بخش مدیریت")]

        public async Task<IActionResult> ManageUser(Guid userId, CancellationToken cancellationToken)
        {
            ActiveMenu("UserManager");
            if (cancellationToken.IsCancellationRequested) return View();

            var getUserForManagment =
                await _userService.GetManagedUserVmAsync(new RequestQueryById(userId));
            return View(getUserForManagment.Data);
        }

        [HttpPost, Route(BaseRouteing.RouteDefaultUserId)]
        [DisplayName("ویرایش بخش مدیریت"), Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<IActionResult> ManageUser(ManagedUserViewModel model)
        {
            if (model.SelectedRole is null)
                _notification.ErrorNotify("کاربر باید حداقل دادای یک نقش باشد", OperationMessageTitleResult.هشــدار, NotificationType.warning);
            model.RolePermission = _userService.GetUserRoleAsSelectListItem(new RequestQueryById(model.Id)).Result.Data;
            if (!ModelState.IsValid) return View(model);

            var result = await _userService.EditManageUserVmAsync(model);
            if (result.IsSuccessed)
            {
                _notification.Notify(OperationMessages.OperationSuccess, OperationMessageTitleResult.موفقیت_آمیز, NotificationType.success, false);
                return RedirectToAction("Index", "UserManager");
            }
            return View(model);
        }
        #endregion
        #region Pagging
        [HttpPost]
        public async Task<IActionResult> GetListPaging()
        {
            Request.GetDataFromRequest(out FiltersFromRequestDataTableBase filtersFromRequest);
            var result = await _userService.GetUserPagination(filtersFromRequest);
            return new JsonResult(result);
        }
        #endregion
        #region UserSettings

        /// <summary>
        /// فعال و غیر فعال کردن فقل حساب کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        public async Task<IActionResult> ChangeLockOutEnable(Guid userId)
        {
            var result = await _userService.ConfigureUserSettingAsync(new RequestQueryById(userId),
                new RequestFieldOperationCommandDto(OperationConfigSettingConstant.LockoutEnabled));
            if (!result.IsSuccessed)
            {
                return NotFound();
            }

            var resultJsonData = !result.Data ? "غیرفعال" : "فعال";
            return Json(resultJsonData);

        }

        /// <summary>
        /// فعال و غیر فعال کردن کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        public async Task<IActionResult> InActiveOrActiveUser(Guid userId)
        {
            var result = await _userService.ConfigureUserSettingAsync(new RequestQueryById(userId),
                new RequestFieldOperationCommandDto(OperationConfigSettingConstant.IsActive));
            if (!result.IsSuccessed)
            {
                return NotFound();
            }

            var resultJsonData = !result.Data ? "غیرفعال" : "فعال";
            return Json(resultJsonData);
        }

        /// <summary>
        /// فعال و غیر فعال کردن احرازهویت دو مرحله ای
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        public async Task<IActionResult> ChangeTwoFactorEnabled(Guid userId)
        {
            var result = await _userService.ConfigureUserSettingAsync(new RequestQueryById(userId),
                new RequestFieldOperationCommandDto(OperationConfigSettingConstant.TwoFactorEnabled));
            if (!result.IsSuccessed)
            {
                return NotFound();
            }

            var resultJsonData = !result.Data ? "غیرفعال" : "فعال";
            return Json(resultJsonData);
        }

        /// <summary>
        /// تایید و عدم تایید وضعیت ایمیل کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        public async Task<IActionResult> ChangeEmailConfirmed(Guid userId)
        {
            var result = await _userService.ConfigureUserSettingAsync(new RequestQueryById(userId),
                new RequestFieldOperationCommandDto(OperationConfigSettingConstant.EmailConfirmed));
            if (!result.IsSuccessed)
            {
                return NotFound();
            }

            var resultJsonData = !result.Data ? "تایید نشده" : "تایید شده";
            return Json(resultJsonData);
        }

        /// <summary>
        /// تایید و عدم تایید وضعیت شماره موبایل کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        public async Task<IActionResult> ChangePhoneNumberConfirmed(Guid userId)
        {
            var result = await _userService.ConfigureUserSettingAsync(new RequestQueryById(userId),
                new RequestFieldOperationCommandDto(OperationConfigSettingConstant.PhoneNumberConfirmed));
            if (!result.IsSuccessed)
            {
                return NotFound();
            }

            var resultJsonData = !result.Data ? "تایید نشده" : "تایید شده";
            return Json(resultJsonData);
        }

        /// <summary>
        /// قفل و خروج از حالت قفل حساب کاربر
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet, Route(BaseRouteing.RouteDefaultUserId)]
        public async Task<IActionResult> LockOrUnLockUserAccount(Guid userId)
        {
            var result = await _userService.ConfigureUserSettingAsync(new RequestQueryById(userId),
                new RequestFieldOperationCommandDto(OperationConfigSettingConstant.LockOrUnLock));
            if (!result.IsSuccessed)
            {
                return NotFound();
            }
            var resultJsonData = !result.Data ? "قفل نشده" : "قفل شده";
            return Json(resultJsonData);
        }
        #endregion
    }
}
