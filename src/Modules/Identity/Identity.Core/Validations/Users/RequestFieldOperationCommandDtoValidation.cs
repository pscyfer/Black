using Common.Application.Validation;
using FluentValidation;
using Identity.Core.Dto.User;

namespace Identity.Core.Validations.Users;

internal class RequestFieldOperationCommandDtoValidation : AbstractValidator<RequestFieldOperationCommandDto>
{
    public RequestFieldOperationCommandDtoValidation()
    {
        RuleFor(x => x.FieldName).NotEmpty()
            .NotNull()
            .WithMessage(ValidationMessages.Required(" نام فیلد"));
    }
}