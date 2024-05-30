namespace Common.AspNetCore.Notification;

public class NotificationDto
{
    public string Title { get; set; } 
    public string Message { get; set; }
    public string Icon { get; set; }

    public bool Interval { get; set; } = default;

    public bool IsQuestion { get; set; } = default;
    public string? MessageQuestion { get; set; } = default;
    public string? Icon2 { get; set; }

    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this);
    }
}