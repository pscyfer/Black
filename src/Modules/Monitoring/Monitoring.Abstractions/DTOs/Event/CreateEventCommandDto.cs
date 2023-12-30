using Monitoring.Core.Enums;

namespace Monitoring.Abstractions.DTOs.Event
{
    public record CreateEventCommandDto(long MonitorId, EventType EventType, string Reason,string Duration);
    public record CreateFirstEventCommandDto(long MonitorId);
}