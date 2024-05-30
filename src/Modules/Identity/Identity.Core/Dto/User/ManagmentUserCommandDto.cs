using Common.Application;
using Common.Domain.Enums;
using Identity.Core.Dto.Role;

namespace Identity.Core.Dto.User;

public class ManagmentUserCommandDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public GenderType Gender { get; set; }
    public string Avatar { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public string BirthDay { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }
    public bool PhoneNumberConfirmed { get; set; }

    public SocialNetworkDto SocialNetwork{ get; set; }
    public List<RoleDto> Roles { get; set; }
}