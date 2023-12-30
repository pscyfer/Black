using Common.Application.Validation;
using FluentValidation;
using Identity.Core.Dto.Shared;

namespace Identity.Core.Validations
{
    internal class RequestQueryByStringValidation : AbstractValidator<RequestQueryByString>
    {
        public RequestQueryByStringValidation()
        {
            RuleFor(x => x.Identifier).NotEmpty()
                .NotNull()
                .WithMessage(ValidationMessages.Required("رشته مورد نظر نباید خالی باشد"))
                ;
        }
    }
}
