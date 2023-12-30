using Common.Application;
using Common.Application.Exceptions;
using Common.Domain.Exceptions;
using Identity.Core;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.Data.Entities;
using Identity.ViewModels.RoleManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.Core.Repository.Roles
{
    public class RoleRepository : RoleManager<Role>, IRoleRepository
    {
        public RoleRepository(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, PersianIdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public async Task<int> TotalCount()
        {
            try
            {
                return await Roles.CountAsync();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

      

        public async Task<Role> FindClaimsInRole(RequestQueryById id)
        {
            return await Roles.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == id.Identifier) ?? throw new NullOrEmptyDomainDataException("role not found !");
        }

        public async Task<IdentityResult> AddOrUpdateRoleClaimAsync(RequestQueryById id, string roleClaimType, IList<string>? selectedRoleClaimValue)
        {
            try
            {
                var role = await FindClaimsInRole(id);
                if (role is null)
                {
                    return IdentityResult.Failed(new IdentityError()
                    {
                        Code = "NotFount",
                        Description = "نقش مورد نظر یافت نشد"
                    });
                }

                var currnetRoleCalimValues =
                    role.Claims.Where(x => x.ClaimType == roleClaimType).Select(x => x.ClaimValue).ToList() ?? new List<string>();
                if (selectedRoleClaimValue is not null)
                {
                    var addNewClaimValues = selectedRoleClaimValue.Except(currnetRoleCalimValues).ToList();

                    foreach (var claimValue in addNewClaimValues)
                    {
                        role.Claims.Add(new RoleClaim()
                        {
                            RoleId = id.Identifier,
                            ClaimType = roleClaimType,
                            ClaimValue = claimValue,
                        });
                    }

                    var removeClaimValue = currnetRoleCalimValues.Except(selectedRoleClaimValue).ToList();
                    foreach (var roleClaim in removeClaimValue.Select(claim => role.Claims.FirstOrDefault(x => x.ClaimValue == claim && x.ClaimType == roleClaimType)).Where(RoleClaim => RoleClaim is not null))
                    {
                        if (roleClaim != null) role.Claims.Remove(roleClaim);
                    }
                }
                else
                    role.Claims.Clear();


                return await UpdateAsync(role);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
