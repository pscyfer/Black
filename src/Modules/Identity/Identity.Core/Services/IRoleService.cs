using Common.Application;
using Common.Application.DataTableConfig;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.ViewModels.RoleManager;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Services
{
    public interface IRoleService
    {
        Task<OperationResult> CreateRole(RequestCreateRoleDto request);
        Task<OperationResult<RoleDto>> GetRole(RequestQueryById request);
        Task<OperationResult<Guid>> UpdateRole(RoleDto role);
        Task<OperationResult<bool>> DeleteRole(RequestQueryById request);
        Task<bool> IsExist(RequestQueryById request);
        Task<OperationResult<GetRoleWithPermissionsDto>> GetRoleWithPermissions(RequestQueryById request);
        Task<PaginationDataTableResult<RoleDto>> GetPagging(FiltersFromRequestDataTableBase request);
        void GetPermission(GetRoleWithPermissionsDto viewModel);

        #region ViewModel

        Task<OperationResult<GetRoleWithPermissionsViewModel>>
            GetRoleWithPermissionsViewModel(RequestQueryById request);

        Task<PaginationDataTableResult<RoleVm>> GetPaggingViewModel(FiltersFromRequestDataTableBase request);
        Task<OperationResult<RoleVm>> GetRoleViewModel(RequestQueryById request);
        Task<OperationResult<RemoveRoleViewModel>> GetRoleForRemove(RequestQueryById request);
        void GetPermissionViewModel(GetRoleWithPermissionsViewModel viewModel);

        #endregion

        IList<string> FindUserPermissions(Guid userId);
        Task<OperationResult<bool>> RemoveRole(RequestQueryById requestQueryById);
        Task<OperationResult> UpdateRolePermission(GetRoleWithPermissionsViewModel request);
        Task<RoleDto> FindClaimsInRole(RequestQueryById request);
        public Task<OperationResult> AddOrUpdateRoleClaimAsync(RequestQueryById requestQueryById, string roleClaimType, IList<string>? selectedRoleClaimValue);
    }
}