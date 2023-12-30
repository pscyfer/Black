using Certificate.Checker.Models;
using Common.Application;
using Monitoring.Abstractions.DTOs.ResponseTime;

namespace Monitoring.UpTimeServices;

public interface IHttpRequestToolsService
{
    Task<OperationResult<GetResponseTimeDto>> GetRequestResponse(string ip, int timeout, Core.Enums.HttpMethod method);
    Task<OperationResult<bool>> CheckIpOrDomainCertificate(CheckRequest request);

    OperationResult<DateTime> CheckDomainExpiration(CheckRequest request);
}