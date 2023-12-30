using Common.Application.Validation;
using Common.Application.Validation.FluentValidations;
using FluentValidation;
using Identity.Core.Dto.User;

namespace Identity.Core.Validations.Users
{
    internal class RequestByPhoneNumberCommandDtoValidator : AbstractValidator<RequestByPhoneNumberCommandDto>
    {
        public RequestByPhoneNumberCommandDtoValidator()
        {
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage(ValidationMessages.Required("شماره موبایل"))
                .ValidPhoneNumber();
        }
    }
}
