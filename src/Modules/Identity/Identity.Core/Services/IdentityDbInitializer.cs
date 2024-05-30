using System.Text;
using Common.Application;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Identity.Core.Dto.Shared;
using Identity.Core.Repository.Roles;
using Identity.Core.Repository.Users;
using Identity.Core.Security;
using Identity.Data;
using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Identity.Core.Services;

public class IdentityDbInitializer : IIdentityDbInitializer
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<IdentityDbInitializer> _logger;
    private readonly IRoleRepository _roleRepository;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;

    public IdentityDbInitializer(IUserRepository userRepository, ILogger<IdentityDbInitializer> logger, IRoleRepository roleRepository, IServiceScopeFactory scopeFactory, IMvcActionsDiscoveryService mvcActionsDiscovery)
    {
        _userRepository = userRepository;
        _logger = logger;
        _roleRepository = roleRepository;
        _scopeFactory = scopeFactory;
        _mvcActionsDiscovery = mvcActionsDiscovery;
    }
    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    public void Initialize()
    {
        using var serviceScope = _scopeFactory.CreateScope();
        using var context = serviceScope.ServiceProvider.GetService<IdentityContext>();
        context?.Database.Migrate();
    }

    /// <summary>
    /// Adds some default values to the IdentityDb
    /// </summary>
    public void SeedData()
    {
        using var serviceScope = _scopeFactory.CreateScope();
        var identityDbSeedData = serviceScope.ServiceProvider.GetService<IIdentityDbInitializer>();
        var result = identityDbSeedData?.SeedDatabaseWithAdminUserAsync().Result;
        if (result == IdentityResult.Failed())
        {
            if (result != null) throw new InvalidOperationException(DumpErrors(result));
        }
    }

    public async Task<IdentityResult> SeedDatabaseWithAdminUserAsync()
    {
        const string userName = "owner";
        const string password = "admin";
        
        var adminUser = await _userRepository.FindByNameAsync(userName);

        if (adminUser != null)
        {
            _logger.LogInformation($"{nameof(SeedDatabaseWithAdminUserAsync)}: adminUser already exists.");
        }
        // Create Role For basicUser `User`
        var getDefaultRoles = StandardRoles.SystemRoles;
        foreach (var role in getDefaultRoles)
        {
            var createRole = await _roleRepository.FindByNameAsync(role);
            if (createRole == null)
            {
                createRole = new Role(role,"");
                var userRoleResult = await _roleRepository.CreateAsync(createRole);
                if (userRoleResult == IdentityResult.Failed())
                {
                    _logger.LogError($"{nameof(SeedDatabaseWithAdminUserAsync)}: UserRole CreateAsync failed. {DumpErrors(userRoleResult)}");
                }
            }
            else
            {
                _logger.LogInformation($"{nameof(SeedDatabaseWithAdminUserAsync)}: UserRole already exists.");
            }
        }

        //AddAllowDynamicPermissionToAdminRole
        var getAllAreaAndControllerAndAction = await
            _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
        var adminRoleId = await _roleRepository.FindByNameAsync(StandardRoles.SuperAdmin);
        var findRoleClaims = await _roleRepository.FindClaimsInRole(new RequestQueryById(adminRoleId.Id));
        if (!findRoleClaims.Claims.Any())
        {
            List<string>? allMvcAction = new();
            foreach (var permission in getAllAreaAndControllerAndAction)
            {
                allMvcAction.AddRange(permission.MvcActions.OrderBy(x => x.ActionDisplayName).Select(actions => actions.ActionId));
            }

            await _roleRepository.AddOrUpdateRoleClaimAsync(new RequestQueryById(adminRoleId.Id), ConstantPolicies.DynamicPermissionClaimType, allMvcAction);
            _logger.LogInformation($"{nameof(SeedDatabaseWithAdminUserAsync)}: RoleClaims already Added.");
        }
        else
        {
            _logger.LogInformation($"{nameof(SeedDatabaseWithAdminUserAsync)}: RoleClaims already exists.");
        }

        adminUser = User.RegisterUserWith(userName);
        var adminUserResult = await _userRepository.CreateAsync(adminUser, password);
        if (adminUserResult == IdentityResult.Failed())
        {
            _logger.LogError($"{nameof(SeedDatabaseWithAdminUserAsync)}: adminUser CreateAsync failed. {DumpErrors(adminUserResult)}");
            return IdentityResult.Failed();
        }

        var setLockoutResult = await _userRepository.SetLockoutEnabledAsync(adminUser, enabled: false);
        if (setLockoutResult == IdentityResult.Failed())
        {
            _logger.LogError($"{nameof(SeedDatabaseWithAdminUserAsync)}: adminUser SetLockoutEnabledAsync failed.");
            return IdentityResult.Failed();
        }

        var addToRoleResult = await _userRepository.AddToRoleAsync(adminUser, StandardRoles.SuperAdmin);
        if (addToRoleResult == IdentityResult.Failed())
        {
            _logger.LogError($"{nameof(SeedDatabaseWithAdminUserAsync)}: adminUser AddToRoleAsync failed. {DumpErrors(addToRoleResult)}");
            return IdentityResult.Failed();
        }
        return IdentityResult.Success;
    }

    public static string DumpErrors(IdentityResult result, bool useHtmlNewLine = false)
    {
        var results = new StringBuilder();
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                var errorDescription = error.Description;
                if (string.IsNullOrWhiteSpace(errorDescription))
                {
                    continue;
                }

                if (!useHtmlNewLine)
                {
                    results.AppendLine(errorDescription);
                }
                else
                {
                    results.AppendLine($"{errorDescription}<br/>");
                }
            }
        }
        return results.ToString();
    }
}