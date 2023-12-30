namespace Common.AspNetCore.Extensions;

public class SweetAlertException:BaseAspNetCoreExceptions
{
    public SweetAlertException() : base("خطایی در  عملیات های پیغام های SweetAlert رخ داده است..")
    {
    }

    public SweetAlertException(string message) : base(message)
    {
    }
}