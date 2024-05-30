using Common.Application.Validation;
using FluentValidation;
using Identity.Core.Dto.User;

namespace Identity.Core.Validations.Users
{
    public class SignInWithPasswordDtoValidator : AbstractValidator<SignInWithPasswordQueryDto>
    {
        public SignInWithPasswordDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.Required("نام کاربری"));
            RuleFor(x => x.Password).NotEmpty().NotNull()
                .WithMessage(ValidationMessages.Required("کلمه عبور"));
        }
    }
}
