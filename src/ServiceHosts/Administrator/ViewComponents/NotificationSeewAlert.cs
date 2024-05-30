using Common.AspNetCore.Notification;
using Microsoft.AspNetCore.Mvc;

namespace Administrator.ViewComponents;

public class NotificationSeewAlert : ViewComponent
{
    private readonly INotification _notification;

    public NotificationSeewAlert(INotification notification)
    {
        _notification = notification;
    }
    public IViewComponentResult Invoke()
    {
        return View(_notification.Read());
    }
}