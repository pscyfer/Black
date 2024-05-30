using Certificate.Checker.Models;
using Common.Application;
using Common.Application.DateUtil;
using Common.Application.Exceptions;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Monitoring.Abstractions.DTOs.ResponseTime;
using Monitoring.Abstractions.Interfaces;
using RestSharp;
using System.Net;

namespace Monitoring.UpTimeServices;

public class UpTimeService : IUpTimeService
{
    private readonly IHttpRequestToolsService _HttpRequestToolsService;
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IHttpRequestMonitoringService _requestMonitoringService;
    private readonly IEventMonitoringService _eventMonitoringService;
    public UpTimeService(IHttpRequestToolsService httpRequestToolsService,
        IBackgroundJobClient backgroundJobClient, IHttpRequestMonitoringService requestMonitoringService, IEventMonitoringService eventMonitoringService)
    {
        _HttpRequestToolsService = httpRequestToolsService;
        _backgroundJobClient = backgroundJobClient;
        _requestMonitoringService = requestMonitoringService;
        _eventMonitoringService = eventMonitoringService;
    }

    public async Task<OperationResult> SendRequest(long monitorId)
    {
        try
        {
            var monitor = await _requestMonitoringService.GetBy(new RequestQueryById<long>(monitorId));
            if (monitor.IsSuccessed)
            {
                var response = await _HttpRequestToolsService.GetRequestResponse(monitor.Data.Ip, monitor.Data.Timeout, (Core.Enums.HttpMethod)monitor.Data.HttpMethod);
                if (response.IsSuccessed)
                {
                    _backgroundJobClient.Enqueue<IResponsTimeService>(x =>
                   x.CreateResponsTime(new CreateResponsTimeDto(monitorId, response.Data.ResponseTime, DateConvertor.DateTimeMilliseconds())));


                    //if (response.Data.StatusCode != HttpStatusCode.OK)
                    //{
                        string duration = _eventMonitoringService.CalculateDurationEvent(new RequestQueryById<long>(monitorId)).Result.Data;
                        await _eventMonitoringService.AddEvent(new Abstractions.DTOs.Event.CreateEventCommandDto(monitorId, Core.Enums.EventType.Down, response.Data.Reason, duration));
                    //}

                    if (monitor.Data.IsDoaminCheck)
                    {
                        var DomainExpirationResult = _HttpRequestToolsService.CheckDomainExpiration(new CheckRequest(monitor.Data.Ip));
                        if (DomainExpirationResult.IsSuccessed)
                            await _requestMonitoringService.UpdateExpireDomainDate(DomainExpirationResult.Data, monitor.Data.Id);

                    }
                    if (monitor.Data.IsSslVerification)
                    {
                        var CheckIpOrDomainCertificateRespons = await _HttpRequestToolsService
                       .CheckIpOrDomainCertificate(new CheckRequest(monitor.Data.Ip));
                    }




                    return OperationResult.Success();
                }

                return OperationResult.Error();

            }

            return OperationResult.NotFound();

        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}