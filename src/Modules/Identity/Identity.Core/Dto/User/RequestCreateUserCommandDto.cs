namespace Identity.Core.Dto.User;

public record RequestCreateUserCommandDto(string UserName, string FirstName, string LastName, string Password);