namespace Monitoring.Abstractions.DTOs.Event
{
    public class GetEventListDto
    {
        public string MonitorName { get; set; }
        public string TypeOfEvent { get; set; }
        public string DateTime { get; set; }
        public string Reason { get; set; }
        public string Duration { get; set; }
    }
}
