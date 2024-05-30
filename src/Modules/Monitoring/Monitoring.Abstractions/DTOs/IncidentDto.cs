namespace Monitoring.Abstractions.DTOs;

public class IncidentDto
{
    public long Id { get; set; }
    public string Cause { get; set; }

    public long StartAt { get; set; }

    public string Duration { get; set; }

    public string ResponsHeader { get; set; }

    public string RequestBody { get; set; }

    public long MonitorId { get; set; }
}