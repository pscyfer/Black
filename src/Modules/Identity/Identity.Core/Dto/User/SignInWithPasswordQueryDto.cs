using Common.Application.Validation;
using FluentValidation;

namespace Identity.Core.Dto.User
{
    public record SignInWithPasswordQueryDto(string UserName, string Password, bool IsRemember = false, bool LockoutOnFailure = false);
}
