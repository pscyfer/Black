namespace Monitoring.Core.Entities;

public class ResponsTimeMonitor
{
    public ResponsTimeMonitor()
    {
        DateTime = DateTime.Now;
    }
    public ResponsTimeMonitor(long monitorId, long responsTime, long CreatedAt)
    {
        MonitorId = monitorId;
        ResponsTime = responsTime;
    }
    public long Id { get; set; }

    public DateTime DateTime { get; set; }

    public long ResponsTime { get; set; }

    public long MonitorId { get; set; }

    public Monitor Monitor { get; set; }
}