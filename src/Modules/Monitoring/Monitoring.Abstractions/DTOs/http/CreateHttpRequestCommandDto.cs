namespace Monitoring.Abstractions.DTOs.http;

public record CreateHttpRequestCommandDto(Guid UserId,string OwnerFullName,
    string Ip,string Name,int Interval,int Timeout,bool IsSslVerification,bool IsDomainCheck);