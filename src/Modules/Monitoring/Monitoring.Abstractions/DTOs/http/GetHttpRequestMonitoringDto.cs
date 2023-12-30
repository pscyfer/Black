namespace Monitoring.Abstractions.DTOs.http;

public class GetHttpRequestMonitoringDto
{
    public long Id { get; set; }
    public string Ip { get; set; }
    public string Name { get; set; }
    public int Interval { get; set; }
    public int Timeout { get; set; }

    public bool IsPause { get; set; }
    public DateTime LastChecked { get; set; }
    public DateTime UpTimeFor { get; set; }

    public int StatusCode { get; set; }
    public bool IsSslVerification { get; set; }
    public bool IsDoaminCheck { get; set; }
    public int HttpMethod { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; }


}

public class GetHttpRequestMonitoringPagginDto
{
    public long Id { get; set; }
    public string Ip { get; set; }
    public string Name { get; set; }
    public int Interval { get; set; }
    public int Timeout { get; set; }

    public bool IsPause { get; set; }
    public DateTime LastChecked { get; set; }
    public DateTime UpTimeFor { get; set; }

    public bool IsSslVerification { get; set; }
    public bool IsDomainCheck { get; set; }
    public string HttpMethod { get; set; }
    public Guid UserId { get; set; }
    public string OwnerFullName { get; set; }


}