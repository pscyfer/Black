using Monitoring.Core.Enums;

namespace Monitoring.Core.Entities;

/// <summary>
/// رخ داد ها
/// </summary>
public class Event
{
    public Event()
    {
        
    }
    public long Id { get; set; }
    public EventType EventType { get; set; }
    public DateTime DateTime { get; set; }


    /// <summary>
    /// دلیل
    /// </summary>
    public string Reason { get; set; }
    public long MonitorId { get; set; }
    public Monitor Monitor { get; set; }


    #region Methods
    public static Event NewInstance() => new Event();

    public void Create(long monitorId, EventType eventType, string reason)
    {
        EventType = eventType;
        Reason = reason;
        MonitorId = monitorId;
    }
    #endregion
}


