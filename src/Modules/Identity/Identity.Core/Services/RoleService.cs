using AutoMapper;
using Common.Application;
using Common.Application.DataTableConfig;
using Common.Application.Exceptions;
using Identity.Core.Dto.Role;
using Identity.Core.Repository.Roles;
using Identity.Core.Security;
using Identity.Core.Utilities;
using Identity.Data;
using Identity.Data.Entities;
using Identity.ViewModels.RoleManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;
using Identity.Core.Dto.Claim;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Identity.Core.Dto.Shared;

namespace Identity.Core.Services;

public class RoleService : IRoleService
{
    #region Ctor

    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    #endregion

    #region Methods

    public async Task<OperationResult> CreateRole(RequestCreateRoleDto request)
    {
        try
        {
            var createRole = await _roleRepository.CreateAsync(new Role(request.RoleName, request.Desciption));
            return createRole.Succeeded ? OperationResult.Success() : OperationResult.Error(createRole.GetErrors());
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<RoleDto>> GetRole(RequestQueryById request)
    {
        try
        {
            var operationResult = new OperationResult<RoleDto>();
            var query = await _roleRepository.FindByIdAsync(request.Identifier.ToString());
            if (query is null) return OperationResult<RoleDto>.NotFound();

            operationResult.Data = new RoleDto()
            {
                Id = query.Id,
                Name = query.Name,
                Description = query.Description
            };
            operationResult.Status = OperationResultStatus.Success;
            return operationResult;
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<RoleVm>> GetRoleViewModel(RequestQueryById request)
    {
        try
        {
            var query = await _roleRepository.FindByIdAsync(request.Identifier.ToString());
            if (query is null) return OperationResult<RoleVm>.NotFound();
            return OperationResult<RoleVm>.Success(new RoleVm()
            {
                RoleId = query.Id,
                Name = query.Name,
                Description = query.Description
            });
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<RemoveRoleViewModel>> GetRoleForRemove(RequestQueryById request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult<RemoveRoleViewModel>.NotFound();
            var query = await _roleRepository.Roles.Select(x => new { x.Id, x.Name })
                .FirstOrDefaultAsync(x => x.Id == request.Identifier);
            return query is null
                ? OperationResult<RemoveRoleViewModel>.NotFound()
                : OperationResult<RemoveRoleViewModel>.Success(new RemoveRoleViewModel(query.Id, query.Name));
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public IList<string> FindUserPermissions(Guid userId)
    {
        throw   new NotImplementedException();
        //var userRolesQuery = from role in _roles
        //                     join userRole in _userRoles on role.Id equals userRole.RoleId
        //                     where userRole.UserId == userId
        //                     select new { role.Name, role.Permissions };

        //var roles = userRolesQuery.AsNoTracking().ToList();
        //var roleNames = roles.Select(a => a.Name).ToList();
        //return
        //    roleNames.Union(
        //        _permissionService.GetUserPermissionsAsList(
        //            roles.Select(a => XElement.Parse(a.Permissions)).ToList())).ToList();
    }
    public async Task<OperationResult<bool>> RemoveRole(RequestQueryById requestQueryById)
    {
        try
        {
            if (!await IsExist(requestQueryById)) return OperationResult<bool>.NotFound();
            var findRole = await _roleRepository.FindByIdAsync(requestQueryById.ToString());
            var query = await _roleRepository.DeleteAsync(findRole);
            if (query.Succeeded)
                return OperationResult<bool>.Success(true);
            return OperationResult<bool>.NotFound();
        }
        catch (BaseApplicationExceptions r)
        {
            Console.WriteLine(r.Message);
            throw;
        }
    }

    public async Task<OperationResult> UpdateRolePermission(GetRoleWithPermissionsViewModel request)
    {
        try
        {
            if (request.PermissionNames.Length < 1)
                return OperationResult.NotFound();
            if (!await IsExist(new RequestQueryById(request.RoleId)))
                return OperationResult.NotFound("Role ID Not Found #Code404");
            var role = await _roleRepository.FindByIdAsync(request.RoleId.ToString());
            if (role != null)
            {
                role.Name = request.Name;
                role.Description = request.Desciption;
                //role.XmlPermission = _permissionService.GetPermissionsAsXml(request.PermissionNames);
                await _roleRepository.UpdateAsync(role);
                return OperationResult.Success();
            }

            return OperationResult.Error("can`t find role for edit this ... #Code404");
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<RoleDto> FindClaimsInRole(RequestQueryById request)
    {
        try
        {
            return _mapper.Map<RoleDto>(await _roleRepository.Roles.AsNoTracking()
                .Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == request.Identifier));
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult> AddOrUpdateRoleClaimAsync(RequestQueryById requestQueryById, string roleClaimType, IList<string>? selectedRoleClaimValue)
    {
        try
        {
            var role = await FindClaimsInRole(requestQueryById);

            var currnetRoleCalimValues =
                role.Claims.Where(x => x.ClaimType == roleClaimType).Select(x => x.ClaimValue).ToList() ?? new List<string>();
            if (selectedRoleClaimValue is not null)
            {
                var addNewClaimValues = selectedRoleClaimValue.Except(currnetRoleCalimValues).ToList();

                foreach (var claimValue in addNewClaimValues)
                {
                    role.Claims.Add(new AddRoleClaimDto()
                    {
                        RoleId = requestQueryById.Identifier,
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
           
            var mapped = _mapper.Map<Role>(role);
            // mapped.Permissions = "";
            await _roleRepository.UpdateAsync(mapped);
            return OperationResult.Success();

        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<Guid>> UpdateRole(RoleDto role)
    {
        try
        {
            var findRole = await _roleRepository.FindByIdAsync(role.Id.ToString());
            if (findRole is null) return OperationResult<Guid>.NotFound();

            findRole.Description = role.Description;
            findRole.Name = role.Name;

            await _roleRepository.UpdateAsync(findRole);
            return OperationResult<Guid>.Success(findRole.Id);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<bool>> DeleteRole(RequestQueryById request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult<bool>.NotFound();
            var role = await _roleRepository.FindByIdAsync(request.Identifier.ToString());
            await _roleRepository.DeleteAsync(role);
            return OperationResult<bool>.Success(true);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



    public async Task<bool> IsExist(RequestQueryById request)
    {
        try
        {
            return await _roleRepository.Roles.AnyAsync(x => x.Id == request.Identifier);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<GetRoleWithPermissionsDto>> GetRoleWithPermissions(RequestQueryById request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult<GetRoleWithPermissionsDto>.NotFound();

            var query = await _roleRepository.FindByIdAsync(request.ToString());
            var getRoleWithPermissionsDto = new GetRoleWithPermissionsDto(roleId: query.Id, name: query.Name);
            getRoleWithPermissionsDto.PermissionNames = Array.Empty<string>();
            //if (!string.IsNullOrWhiteSpace(query.Permissions))
            //    getRoleWithPermissionsDto.PermissionNames =
            //        _permissionService.GetUserPermissionsAsList(query.XmlPermission).ToArray();

            return OperationResult<GetRoleWithPermissionsDto>.Success(getRoleWithPermissionsDto);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OperationResult<GetRoleWithPermissionsViewModel>> GetRoleWithPermissionsViewModel(
        RequestQueryById request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult<GetRoleWithPermissionsViewModel>.NotFound();

            var query = await _roleRepository.FindByIdAsync(request.ToString());
            var getRoleWithPermissionsViewModel =
                new GetRoleWithPermissionsViewModel(roleId: query.Id, name: query.Name, query.Description);
            getRoleWithPermissionsViewModel.PermissionNames = Array.Empty<string>();
            //if (!string.IsNullOrWhiteSpace(query.Permissions))
            //    getRoleWithPermissionsViewModel.PermissionNames =
            //        _permissionService.GetUserPermissionsAsList(query.XmlPermission).ToArray();
            return OperationResult<GetRoleWithPermissionsViewModel>.Success(getRoleWithPermissionsViewModel);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PaginationDataTableResult<RoleDto>> GetPagging(FiltersFromRequestDataTableBase request)
    {
        try
        {
            var query = _roleRepository.Roles.AsNoTracking();
            if (!(string.IsNullOrEmpty(request.sortColumn) && string.IsNullOrEmpty(request.sortColumnDirection)))
            {
                query = query.OrderBy(request.sortColumn + " " + request.sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(request.searchValue))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.searchValue) || x.Description.Contains(request.searchValue));
            }

            int recordsFiltered = await query.CountAsync();
            var queryResult = await query.Skip(request.skip).Take(request.pageSize).ToListAsync();
            var lists = _mapper.Map<List<RoleDto>>(queryResult);
            int recordsTotal = await _roleRepository.TotalCount();
            PaggingDataTableExtention.ConfigPaging(ref request, recordsTotal);
            var result = new PaginationDataTableResult<RoleDto>()
            {
                draw = request.draw,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal,
                data = lists
            };
            return result;
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void GetPermission(GetRoleWithPermissionsDto viewModel)
    {
        var permissions = AssignableToRolePermissions.GetAsSelectListItems();
        var selectListItems = permissions as IList<SelectListItem> ?? permissions.ToList();
        selectListItems.ToList().ForEach(
            a => a.Selected = viewModel.PermissionNames.Any(s => s == a.Value));

        viewModel.Permissions = selectListItems;
    }

    public void GetPermissionViewModel(GetRoleWithPermissionsViewModel viewModel)
    {
        var permissions = AssignableToRolePermissions.GetAsSelectListItems();
        var selectListItems = permissions as IList<SelectListItem> ?? permissions.ToList();
        selectListItems.ToList().ForEach(
            a => a.Selected = viewModel.PermissionNames.Any(s => s == a.Value));

        viewModel.Permissions = selectListItems;
    }

    public async Task<PaginationDataTableResult<RoleVm>> GetPaggingViewModel(FiltersFromRequestDataTableBase request)
    {
        try
        {
            var query = _roleRepository.Roles.AsNoTracking();
            if (!(string.IsNullOrEmpty(request.sortColumn) && string.IsNullOrEmpty(request.sortColumnDirection)))
            {
                query = query.OrderBy(request.sortColumn + " " + request.sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(request.searchValue))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.searchValue) || x.Description.Contains(request.searchValue));
            }

            int recordsFiltered = await query.CountAsync();
            var queryResult = await query.Skip(request.skip).Take(request.pageSize).ToListAsync();
            var lists = _mapper.Map<List<RoleVm>>(queryResult);
            int recordsTotal = await _roleRepository.TotalCount();
            PaggingDataTableExtention.ConfigPaging(ref request, recordsTotal);
            var result = new PaginationDataTableResult<RoleVm>()
            {
                draw = request.draw,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal,
                data = lists
            };
            return result;
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    #endregion
}