namespace Common.Domain.Exceptions;

public class ArgumentIsNullOrEmptyException: BaseDomainException
{
    public ArgumentIsNullOrEmptyException()
    {

    }
    public ArgumentIsNullOrEmptyException(string message) : base(message)
    {

    }
}