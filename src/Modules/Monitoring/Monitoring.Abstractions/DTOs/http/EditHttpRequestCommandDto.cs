namespace Monitoring.Abstractions.DTOs.http;

public record EditHttpRequestCommandDto(long Id,
    string Ip, string Name, int Interval, int Timeout, bool IsSslVerification, bool IsDomainCheck);