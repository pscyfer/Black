namespace Monitoring.Abstractions.DTOs.http;

public class GetForEditHttoRequestQueryDto
{
    public long Id { get; set; }
    public string Ip { get; set; }
    public string Name { get; set; }
    public int Interval { get; set; }
    public int Timeout { get; set; }
    public bool IsSslVerification { get; set; }
    public bool IsDoaminCheck { get; set; }
}