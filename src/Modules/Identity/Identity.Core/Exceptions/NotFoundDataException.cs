namespace Identity.Core.Exceptions;

public class NotFoundDataException : ApplicationException
{
    public NotFoundDataException()
    {

    }

    public NotFoundDataException(string message)
    : base(message)
    {

    }
}