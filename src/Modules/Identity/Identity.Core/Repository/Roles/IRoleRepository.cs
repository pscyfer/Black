using Common.Application;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Core.Repository.Roles
{
    public interface IRoleRepository
    {
        #region BaseClass
        IQueryable<Role> Roles { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IList<IRoleValidator<Role>> RoleValidators { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        Task<Role?> FindByIdAsync(string roleId);
        Task<Role?> FindByNameAsync(string roleName);
        string NormalizeKey(string key);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(Role role);
        Task UpdateNormalizedRoleNameAsync(Role role);
        Task<string?> GetRoleNameAsync(Role role);
        Task<IdentityResult> SetRoleNameAsync(Role role, string name);
        #endregion
        #region MyMethods
        Task<int> TotalCount();
        Task<Role> FindClaimsInRole(RequestQueryById id);

        Task<IdentityResult> AddOrUpdateRoleClaimAsync(RequestQueryById id, string roleClaimType,
            IList<string>? selectedRoleClaimValue);
        #endregion



    }
}
