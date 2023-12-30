using Common.Application;
using Common.Application.DataTableConfig;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.Core.Dto.User;
using Identity.ViewModels.RoleManager;
using Identity.ViewModels.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Core.Services
{
    public interface IUserService
    {
        Task<OperationResult<Guid>> RegisterUser(RegisterUserCommandDto command);
        Task<OperationResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber);
        Task<OperationResult<UserDto>> GetUserById(Guid userId);
        Task<OperationResult<SignInResult>> SignInWithPassword(SignInWithPasswordQueryDto command);
        Task<PaginationDataTableResult<UserDto>> GetUserPagination(FiltersFromRequestDataTableBase request);
        Task<OperationResult<GetUserForUpdateDto>> GetUserForEdit(RequestQueryById request);
        Task<OperationResult<int>> GetUserCountBy(bool activeOrNotActive = true, CancellationToken cancellationToken = default);
        Task<OperationResult<int>> GetAllUserCount(CancellationToken cancellationToken = default);
        Task<OperationResult<int>> GetAllNotConfirmAccountCount(CancellationToken cancellationToken = default);
        Task<OperationResult<GetUserForRemoveDto>> GetUserForRemove(RequestQueryById request);
        Task<OperationResult> RemoveUser(RequestQueryById request);
        Task SignOut();
        Task<OperationResult<Guid>> EditUserAsync(UpdateUserCommandDto request);
        Task<OperationResult<ManagmentUserCommandDto>> GetForManageUesrAsync(RequestQueryById request);
        Task<OperationResult> EditManageUserCommandAsync(ManagmentUserCommandDto command);
        Task<OperationResult> CreateUserAsync(RequestCreateUserCommandDto command);
        Task<OperationResult<List<GetUserRoleDto>>> GetUserRole(RequestQueryById request);
        Task<OperationResult<List<SelectListItem>>> GetUserRoleAsSelectListItem(RequestQueryById request);
        Task RefreshSignInAsync(RequestQueryById request);
        #region WithViewModels

        Task<OperationResult<List<GetUserRoleViewModel>>> GetUserRoleViewModel(RequestQueryById request);
        Task<OperationResult<Guid>> EditUserAsync(UserEditViewModel command);
        Task<OperationResult<ManagedUserViewModel>> GetManagedUserVmAsync(RequestQueryById request);
        Task<OperationResult> EditManageUserVmAsync(ManagedUserViewModel command);
        Task<OperationResult> CreateUserAsync(RequestCreateUserViewModel command);
        #endregion

        #region UserSetting
        /// <summary>
        /// This Method for Worked Boolean Field like IsActiveOrEmaliConfrimedOrLockOutEnable...
        /// </summary>
        /// <param name="request">userId</param>
        /// <param name="requestField">NameOf Field Name</param>
        /// <returns></returns>
        Task<OperationResult<bool>> ConfigureUserSettingAsync(RequestQueryById request, RequestFieldOperationCommandDto requestField);
        Task<OperationResult<UserDto>> FindByUserName(RequestQueryByString request);

        #endregion
    }


}
