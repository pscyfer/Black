using Common.Application;
using Common.Application.Validation;
using FluentValidation;
using Identity.Core.Dto.Shared;

namespace Identity.Core.Validations.Users;

public class RequestQueryByIdValidation : AbstractValidator<RequestQueryById>
{
    public RequestQueryByIdValidation()
    {
        RuleFor(x => x.Identifier).NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required("UserId"));
    }
}