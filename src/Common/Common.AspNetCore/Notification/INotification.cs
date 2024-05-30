namespace Common.AspNetCore.Notification;

public interface INotification
{
    void SuccessNotify(string message, OperationMessageTitleResult operationMessageTitle=OperationMessageTitleResult.موفقیت_آمیز,
        NotificationType notificationType = NotificationType.success);
    void ErrorNotify(string message, OperationMessageTitleResult operationMessageTitle=OperationMessageTitleResult.خطاا,
        NotificationType notificationType = NotificationType.error);

    void Notify(string message, OperationMessageTitleResult operationMessageTitle,
        NotificationType notificationType, bool interval = false);

    void MultiyOperationNotify(string message, OperationMessageTitleResult operationMessageTitle,
        NotificationType notificationType = NotificationType.success, bool interval = false,
        bool isQuestion = false, string msgQuestion = "پیغام دوم",
        NotificationType notificationType2 = NotificationType.success);

    NotificationDto Read();    

}