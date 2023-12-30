using AutoMapper;
using Common.Application;
using Common.Application.Consts;
using Common.Application.DataTableConfig;
using Common.Application.DateUtil;
using Common.Application.Exceptions;
using Common.Domain.ValueObjects;
using Identity.Core.Dto.Role;
using Identity.Core.Dto.Shared;
using Identity.Core.Dto.User;
using Identity.Core.Exceptions;
using Identity.Core.Repository.Roles;
using Identity.Core.Repository.Users;
using Identity.Core.Utilities;
using Identity.Data.Entities;
using Identity.ViewModels.RoleManager;
using Identity.ViewModels.UserManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Linq.Dynamic.Core;

namespace Identity.Core.Services
{
    public class UserServices : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserServices> _logger;

        public UserServices(IUserRepository userRepo, IMapper mapper, SignInManager<User> signInManager,
            ILogger<UserServices> logger, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _signInManager = signInManager;
            _logger = logger;
            _roleRepo = roleRepo;
        }

        public async Task<OperationResult<UserDto?>> GetUserByPhoneNumber(string phoneNumber)
        {
            try
            {
                var getUser = await _userRepo.FindByPhoneNumberAsync(phoneNumber, default);
                if (getUser != null)
                {
                    var mapped = _mapper.Map<UserDto>(getUser);
                    return OperationResult<UserDto>.Success(mapped);
                }

                return OperationResult<UserDto?>.NotFound();
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<UserDto>> GetUserById(Guid userId)
        {
            try
            {
                var getUser = await _userRepo.FindByIdAsync(userId.ToString());
                if (getUser != null)
                {
                    var mapped = _mapper.Map<UserDto>(getUser);
                    return OperationResult<UserDto>.Success(mapped);
                }

                return OperationResult<UserDto?>.NotFound();
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<Guid>> RegisterUser(RegisterUserCommandDto command)
        {
            try
            {
                var user = User.RegisterUserWith(command.PhoneNumber);
                var result = await _userRepo.CreateAsync(user, command.Password);
                if (result.Succeeded) return OperationResult<Guid>.Success(user.Id);
                var errors = result.Errors.Select(d => d.Description).ToList();
                string joinErrors = string.Join(", ", errors);
                return OperationResult<Guid>.Error(joinErrors);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<SignInResult>> SignInWithPassword(SignInWithPasswordQueryDto request)
        {
            try
            {
                var operationResult = new OperationResult<SignInResult>();
                if (!await IsExist(request.UserName)) return OperationResult<SignInResult>.NotFound();
                var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password,
                    request.IsRemember,
                    request.LockoutOnFailure);
                return OperationResult<SignInResult>.Success(result);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        private async Task<bool> IsExist(string userName)
        {
            try
            {
                return await _userRepo.Users.AsNoTracking().AnyAsync(x => x.UserName == userName);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        private async Task<bool> IsExist(RequestQueryById request)
        {
            try
            {
                return await _userRepo.Users.AsNoTracking().AnyAsync(x => x.Id == request.Identifier);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<GetUserForRemoveDto>> GetUserForRemove(RequestQueryById request)
        {
            try
            {
                var findUser = await _userRepo.Users.AsNoTracking()
                    .Select(x => new { x.Id, FullName = x.FirstName + " " + x.LastName })
                    .FirstOrDefaultAsync(x => x.Id == request.Identifier);

                return findUser is null
                    ? OperationResult<GetUserForRemoveDto>.NotFound()
                    : OperationResult<GetUserForRemoveDto>.Success(new GetUserForRemoveDto(findUser.Id,
                        findUser.FullName));
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<List<GetUserRoleDto>>> GetUserRole(RequestQueryById request)
        {
            try
            {
                if (!await IsExist(request)) return OperationResult<List<GetUserRoleDto>>.NotFound();
                var roles = await _userRepo.Users.AsNoTracking().Include(x => x.Roles)
                    .Select(x => new { x.Id, x.Roles })
                    .FirstOrDefaultAsync(x => x.Id == request.Identifier);
                roles.Roles.TryGetNonEnumeratedCount(out int result);
                var mapped = _mapper.Map<List<GetUserRoleDto>>(roles.Roles);
                if (result != 0)
                {
                    return OperationResult<List<GetUserRoleDto>>.Success(mapped);
                }

                return OperationResult<List<GetUserRoleDto>>.EmptyList();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<OperationResult<List<GetUserRoleViewModel>>> GetUserRoleViewModel(RequestQueryById request)
        {
            try
            {
                if (!await IsExist(request)) return OperationResult<List<GetUserRoleViewModel>>.NotFound();
                var roles = await _userRepo.Users.AsNoTracking().Include(x => x.Roles)
                    .Select(x => new { x.Id, x.Roles })
                    .FirstOrDefaultAsync(x => x.Id == request.Identifier);
                roles.Roles.TryGetNonEnumeratedCount(out int result);
                var mapped = _mapper.Map<List<GetUserRoleViewModel>>(roles.Roles);
                if (result != 0)
                {
                    return OperationResult<List<GetUserRoleViewModel>>.Success(mapped);
                }

                return OperationResult<List<GetUserRoleViewModel>>.EmptyList();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<List<SelectListItem>>> GetUserRoleAsSelectListItem(RequestQueryById request)
        {
            try
            {
                var getAllRoles = await _roleRepo.Roles.AsNoTracking().ToListAsync();
                var getUserRole =
                    await _userRepo.GetRolesAsync(await _userRepo.FindByIdAsync(request.Identifier.ToString()));
                var selectListItems = getAllRoles.Select(role => new SelectListItem(role.Name, role.Name.ToString())).ToList();
                foreach (var item in getUserRole)
                {
                    if (string.IsNullOrWhiteSpace(item))
                        throw new InvalidNullOrEmptyRoleNameException("role name is empty");
                    var find = selectListItems.FirstOrDefault(x => x.Text == item);

                    if (find != null) find.Selected = true;
                }

                return OperationResult<List<SelectListItem>>.Success(selectListItems);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<OperationResult> RemoveUser(RequestQueryById request)
        {
            try
            {
                if (!await IsExist(request)) return OperationResult.NotFound();
                var updateUser = await _userRepo.FindByIdAsync(request.ToString());
                updateUser?.DeleteUser();
                if (updateUser != null) await _userRepo.UpdateAsync(updateUser);
                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<OperationResult<Guid>> EditUserAsync(UpdateUserCommandDto command)
        {
            try
            {
                var findUser = await _userRepo.FindByIdAsync(command.UserId.ToString());
                if (findUser is null) return OperationResult<Guid>.NotFound();
                findUser.UpdateUser(command.UserName, command.FirstName, command.LastName,
                    command.PhoneNumber, command.Email, command.IsActive, command.BirthDay.ToMiladi(), command.Avatar,
                    command.Gender);

                await _userRepo.UpdateAsync(findUser);

                _logger.LogInformation($"UserService___{nameof(EditUserAsync)}___UserId={findUser.Id}");
                return OperationResult<Guid>.Success(findUser.Id);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<ManagmentUserCommandDto>> GetForManageUesrAsync(RequestQueryById request)
        {
            try
            {
                var user = await _userRepo.FindByIdAsync(request.Identifier.ToString());
                if (user is null)
                    return OperationResult<ManagmentUserCommandDto>.NotFound();

                var resultDto = _mapper.Map<ManagmentUserCommandDto>(user);

                var listOfUserInRoles = await _userRepo.GetRolesAsync(user);
                resultDto.Roles = _mapper.Map<List<RoleDto>>(listOfUserInRoles);

                return OperationResult<ManagmentUserCommandDto>.Success(resultDto);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> EditManageUserCommandAsync(ManagmentUserCommandDto command)
        {
            try
            {
                var user = await _userRepo.FindByIdAsync(command.Id.ToString());
                if (user is null) return OperationResult.NotFound();
                user.UpdateUser(command.UserName, command.FirstName, command.LastName,
                    command.PhoneNumber, command.Email, command.IsActive, command.BirthDay.ToMiladi(), command.Avatar,
                    command.Gender);
                var mapped = _mapper.Map<SocialNetwork>(command.SocialNetwork);
                user.AddSocialNetwork(mapped);

                await _userRepo.UpdateAsync(user);
                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> CreateUserAsync(RequestCreateUserCommandDto command)
        {
            try
            {
                var createUser = new User();
                createUser.Create(command.UserName, command.FirstName, command.LastName);
                var result = await _userRepo.CreateAsync(createUser, command.Password);

                _logger.LogInformation("UserService__CreateUser_UserId={0}__ResultIsCraeted={1}", createUser.Id,
                    result.Succeeded);

                return result.Succeeded ? OperationResult.Success() : OperationResult.Error(result.GetErrors());
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<Guid>> EditUserAsync(UserEditViewModel command)
        {
            try
            {
                var findUser = await _userRepo.FindByIdAsync(command.UserId.ToString());
                if (findUser is null) return OperationResult<Guid>.NotFound();
                findUser.UpdateUser(command.UserName, command.FirstName, command.LastName,
                    command.PhoneNumber, command.Email, command.IsActive, command.BirthDay.ToMiladi(), command.Avatar,
                    command.Gender);

                await _userRepo.UpdateAsync(findUser);

                _logger.LogInformation($"UserService___{nameof(EditUserAsync)}___UserId={findUser.Id}");
                return OperationResult<Guid>.Success(findUser.Id);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<ManagedUserViewModel>> GetManagedUserVmAsync(RequestQueryById request)
        {
            try
            {
                var user = await _userRepo.FindByIdAsync(request.Identifier.ToString());
                if (user is null)
                    return OperationResult<ManagedUserViewModel>.NotFound();

                var resultDto = _mapper.Map<ManagedUserViewModel>(user);

                var listOfUserInRoles = await GetUserRoleAsSelectListItem(request);
                resultDto.RolePermission = listOfUserInRoles.Data;

                return OperationResult<ManagedUserViewModel>.Success(resultDto);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> EditManageUserVmAsync(ManagedUserViewModel command)
        {
            try
            {
                var user = await _userRepo.FindByIdAsync(command.Id.ToString());
                if (user is null) return OperationResult.NotFound();
                user.UpdateUser(command.UserName, command.FirstName, command.LastName,
                    command.PhoneNumber, command.Email, command.IsActive, command.BirthDay.ToMiladi(), command.Gender);
                var mapped = _mapper.Map<SocialNetwork>(command.SocialNetwork);
                user.AddSocialNetwork(mapped);
                await _userRepo.UpdateAsync(user);
                //RemoveRole And Set New Role
                var userRoles = await _userRepo.GetRolesAsync(user);
                await _userRepo.RemoveFromRolesAsync(user, userRoles);
                //await RefreshSignInAsync(user);
                command.SelectedRole.TryGetNonEnumeratedCount(out int seleteRoleCount);
                if (seleteRoleCount > 0)
                {
                    await _userRepo.AddToRolesAsync(user, command.SelectedRole);

                }

                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult> CreateUserAsync(RequestCreateUserViewModel command)
        {
            try
            {
                var createUser = new User();
                createUser.Create(command.UserName, command.FirstName, command.LastName);
                var result = await _userRepo.CreateAsync(createUser, command.Password);

                _logger.LogInformation("UserService__CreateUser_UserId={0}__ResultIsCraeted={1}", createUser.Id,
                    result.Succeeded);

                return result.Succeeded ? OperationResult.Success() : OperationResult.Error(result.GetErrors());
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<OperationResult<bool>> ConfigureUserSettingAsync(RequestQueryById request,
            RequestFieldOperationCommandDto requestField)
        {
            try
            {
                if (!await IsExist(request)) return OperationResult<bool>.NotFound();

                var user = await _userRepo.FindByIdAsync(request.Identifier.ToString());
                if (user is null) return OperationResult<bool>.NotFound();
                var resultValue = false;
                switch (requestField.FieldName)
                {
                    case OperationConfigSettingConstant.IsActive:
                        {
                            resultValue = !user.IsActive;
                            user.IsActive = resultValue;
                        }
                        break;
                    case OperationConfigSettingConstant.EmailConfirmed:
                        {
                            resultValue = !user.EmailConfirmed;
                            user.EmailConfirmed = resultValue;
                        }
                        break;
                    case OperationConfigSettingConstant.PhoneNumberConfirmed:
                        {
                            resultValue = !user.PhoneNumberConfirmed;
                            user.PhoneNumberConfirmed = resultValue;
                        }
                        break;
                    case OperationConfigSettingConstant.LockoutEnabled:
                        {
                            resultValue = !user.LockoutEnabled;
                            user.LockoutEnabled = resultValue;
                        }
                        break;
                    case OperationConfigSettingConstant.TwoFactorEnabled:
                        {
                            resultValue = !user.TwoFactorEnabled;
                            user.TwoFactorEnabled = resultValue;
                        }
                        break;
                    case OperationConfigSettingConstant.LockOrUnLock:
                        {
                            if (user.LockoutEnd == null)
                            {
                                resultValue = true;
                                user.LockoutEnd = DateTime.UtcNow.AddMinutes(10);
                            }
                            else
                            {
                                if (user.LockoutEnd > DateTime.Now)
                                {
                                    resultValue = false;
                                    user.LockoutEnd = null;
                                }
                                else
                                {
                                    resultValue = true;
                                    user.LockoutEnd = DateTime.UtcNow.AddMinutes(10);
                                }
                            }
                        }
                        break;
                }

                await _userRepo.UpdateAsync(user);
                _logger.LogInformation($"UserService__UpdateUserSettingConfig__Date_{DateTime.Now}__");
                return OperationResult<bool>.Success(resultValue);
            }
            catch (BaseApplicationExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<PaginationDataTableResult<UserDto>> GetUserPagination(FiltersFromRequestDataTableBase request)
        {
            try
            {
                var query = _userRepo.Users.AsNoTracking();
                if (!(string.IsNullOrEmpty(request.sortColumn) && string.IsNullOrEmpty(request.sortColumnDirection)))
                {
                    query = query.OrderBy(request.sortColumn + " " + request.sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(request.searchValue))
                {
                    query = query.Where(x =>
                        x.FirstName.Contains(request.searchValue) || x.LastName.Contains(request.searchValue));
                }

                int recordsFiltered = await query.CountAsync();
                var queryResult = await query.Skip(request.skip).Take(request.pageSize).ToListAsync();
                List<UserDto> userDtoList = _mapper.Map<List<UserDto>>(queryResult);
                int recordsTotal = await _userRepo.TotalCount();
                PaggingDataTableExtention.ConfigPaging(ref request, recordsTotal);
                var result = new PaginationDataTableResult<UserDto>()
                {
                    draw = request.draw,
                    recordsFiltered = recordsFiltered,
                    recordsTotal = recordsTotal,
                    data = userDtoList
                };
                return result;
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<GetUserForUpdateDto>> GetUserForEdit(RequestQueryById request)
        {
            try
            {
                var findUser = await _userRepo.Users
                    .Include(c => c.Roles)
                    .SingleOrDefaultAsync(x => x.Id == request.Identifier);

                if (findUser is null) return OperationResult<GetUserForUpdateDto>.NotFound();

                var mapped = _mapper.Map<GetUserForUpdateDto>(findUser);

                return OperationResult<GetUserForUpdateDto>.Success(mapped);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<int>> GetUserCountBy(bool activeOrNotActive = true,
            CancellationToken cancellationToken = default)
        {
            try
            {
                int result = await _userRepo.Users.CountAsync(x => x.IsActive == activeOrNotActive, cancellationToken);
                return OperationResult<int>.Success(result);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<int>> GetAllUserCount(CancellationToken cancellationToken)
        {
            try
            {
                int result = await _userRepo.Users.CountAsync(cancellationToken);
                return OperationResult<int>.Success(result);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }


        public async Task<OperationResult<int>> GetAllNotConfirmAccountCount(CancellationToken cancellationToken)
        {
            try
            {
                int result = await _userRepo.Users.CountAsync(x => x.PhoneNumberConfirmed && x.EmailConfirmed,
                    cancellationToken);
                return OperationResult<int>.Success(result);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }

        public async Task<OperationResult<UserDto>> FindByUserName(RequestQueryByString request)
        {
            try
            {
                var find = await _userRepo.FindByNameAsync(request.ToString());
                if (find is null) return OperationResult<UserDto>.NotFound();

                var mapped = _mapper.Map<UserDto>(find);

                return OperationResult<UserDto>.Success(mapped);
            }
            catch (BaseApplicationExceptions)
            {
                throw;
            }
        }


        public async Task RefreshSignInAsync(RequestQueryById request)
        {
            var user = await _userRepo.FindByIdAsync(request.ToString());
            await _signInManager.RefreshSignInAsync(user);
        }
        private async Task RefreshSignInAsync(User user)
        {
            await _signInManager.RefreshSignInAsync(user);
        }
    }
}