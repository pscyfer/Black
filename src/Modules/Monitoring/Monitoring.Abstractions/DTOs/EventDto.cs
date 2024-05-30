namespace Monitoring.Abstractions.DTOs;

public class EventDto
{
    public long Id { get; set; }
    public string EventType { get; set; }
    public long DateTime { get; set; }


    /// <summary>
    /// دلیل
    /// </summary>
    public string Reason { get; set; }

    public string Duration { get; set; }

    public long MonitorId { get; set; }
}