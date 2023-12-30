using Common.Domain.Enums;

namespace Identity.Core.Dto.User;

public record UpdateUserCommandDto(Guid UserId, string UserName, string FirstName,
    string LastName, string PhoneNumber, string Email, string BirthDay, bool IsActive, GenderType Gender, string Avatar);
