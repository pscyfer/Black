using Common.Application.Validation;
using FluentValidation;
using Identity.Core.Dto.Role;

namespace Identity.Core.Validations.Role;

public class RequestCreateRoleDtoValidation : AbstractValidator<RequestCreateRoleDto>
{
    public RequestCreateRoleDtoValidation()
    {
        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage(ValidationMessages.Required("نام نقش"))
            .NotNull().WithMessage(ValidationMessages.Required("نام نقش"))
            .WithMessage(ValidationMessages.Required("نام نقش"));

    }
}