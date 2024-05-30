using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using Common.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace Common.AspNetCore.Notification;

public class SweetAlertNotifcation : INotification
{
    #region Ctor
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string CookieName = "sysAlertSweet";
    public SweetAlertNotifcation(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    #endregion

    #region Methods
    public void SuccessNotify(string message, OperationMessageTitleResult operationMessageTitle=OperationMessageTitleResult.هشــدار,
       NotificationType notificationType = NotificationType.success)
    {
        try
        {
            var msg = new NotificationDto()
            {
                Message = message,
                Title = operationMessageTitle.ToString(),
                Icon = notificationType.ToString(),
            };

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, JsonSerializer.Serialize(msg), options);
        }
        catch (BaseAspNetCoreExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void ErrorNotify(string message, OperationMessageTitleResult operationMessageTitle=OperationMessageTitleResult.خطاا,
        NotificationType notificationType = NotificationType.error)
    {
        try
        {
            var msg = new NotificationDto()
            {
                Message = message,
                Title = operationMessageTitle.ToString(),
                Icon = notificationType.ToString(),
            };

            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, JsonSerializer.Serialize(msg), options);
        }
        catch (BaseAspNetCoreExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Notify(string message, OperationMessageTitleResult operationMessageTitle, NotificationType notificationType,
        bool interval = false)
    {
        try
        {
            var msg = new NotificationDto()
            {
                Message = message,
                Title = operationMessageTitle.ToString(),
                Icon = notificationType.ToString(),
                Interval = interval
            };

            //CookieOptions options = new CookieOptions
            //{
            //    Expires = DateTime.Now.AddMinutes(1)
            //};
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, JsonSerializer.Serialize(msg)/*, options*/);
        }
        catch (BaseAspNetCoreExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void MultiyOperationNotify(string message, OperationMessageTitleResult operationMessageTitle,
        NotificationType notificationType = NotificationType.success, bool interval = false, bool isQuestion = false,
        string msgQuestion = "پیغام دوم", NotificationType notificationType2 = NotificationType.success)
    {
        try
        {
            var msg = new NotificationDto()
            {
                Message = message,
                Title = operationMessageTitle.ToString(),
                Icon = notificationType.ToString(),
                Interval = interval,
                IsQuestion = isQuestion,
                MessageQuestion = msgQuestion,
                Icon2 = notificationType2.ToString()
            };
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(1)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieName, JsonSerializer.Serialize(msg), options);
        }
        catch (BaseAspNetCoreExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public NotificationDto Read()
    {
        try
        {
            var message = new NotificationDto();
            string getMessagesFormCookie = GetCookieValueFromResponse(
                _httpContextAccessor.HttpContext.Response, CookieName);

            getMessagesFormCookie = WebUtility.UrlDecode(getMessagesFormCookie);

            if (!string.IsNullOrWhiteSpace(getMessagesFormCookie))
            {
                var result = JsonSerializer.Deserialize<NotificationDto>(getMessagesFormCookie);
                message = result;
            }
            else
            {
               _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(CookieName,out string result);
                if(result is not null)
                {
                    var resultSerilize = JsonSerializer.Deserialize<NotificationDto>(result);
                    message = resultSerilize;
                }
              
            }
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieName);
            return message;
        }
        catch (SweetAlertException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    string GetCookieValueFromResponse(HttpResponse response, string cookieName)
    {
        var cookieSetHeader = response.GetTypedHeaders().SetCookie;
        if (cookieSetHeader != null)
        {
            var setCookie = cookieSetHeader.FirstOrDefault(x => x.Name == cookieName);
            if (setCookie is null) return "";
            return setCookie.Value.ToString();
        }
        return null;
    }
    #endregion

}