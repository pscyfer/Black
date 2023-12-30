using Common.Application.Exceptions;

namespace Monitoring.Exceptions;
internal class GetExpirationDateDomainException : BaseApplicationExceptions
{
    public GetExpirationDateDomainException()
    {
    }

    public GetExpirationDateDomainException(string message)
        : base(message)
    {

    }
}
