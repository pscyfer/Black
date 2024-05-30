namespace Monitoring.Core.Entities;

/// <summary>
/// حوادث
/// </summary>
public class Incident
{
    public long Id { get; set; }
    public string Cause { get; set; }

    public DateTime StartAt { get; set; }

    public string Duration { get; set; }

    public string ResponseHeader { get; set; }

    public string RequestBody { get; set; }

    public long MonitorId { get; set; }
    public Monitor Monitor { get; set; }
}