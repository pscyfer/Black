namespace Common.AspNetCore.Extensions;

public class BaseAspNetCoreExceptions : Exception
{
    public BaseAspNetCoreExceptions():base("Common.AspnetCore.Exception")
    {

    }

    public BaseAspNetCoreExceptions(string message) : base(message)
    {

    }
}