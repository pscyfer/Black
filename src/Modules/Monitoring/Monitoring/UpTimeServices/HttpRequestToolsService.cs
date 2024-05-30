using Certificate.Checker.Interfaces;
using Certificate.Checker.Models;
using Common.Application;
using Common.Application.Exceptions;
using Microsoft.Extensions.Logging;
using Monitoring.Abstractions.DTOs.ResponseTime;
using Monitoring.DomainExpirationChecker.Interface;
using Monitoring.Exceptions;
using RestSharp;
using System.Diagnostics;

namespace Monitoring.UpTimeServices;

public class HttpRequestToolsService : IHttpRequestToolsService
{
    private readonly ICertificateCheckerService _certificateChecker;
    private readonly IDomainExpirationCheckerService _domainExpirationChecker;
    private readonly ILogger<HttpRequestToolsService> _logger;

    public HttpRequestToolsService(ICertificateCheckerService certificateChecker,
        IDomainExpirationCheckerService domainExpirationChecker, ILogger<HttpRequestToolsService> logger)
    {
        _certificateChecker = certificateChecker;
        _domainExpirationChecker = domainExpirationChecker;
        _logger = logger;
    }

    public OperationResult<DateTime> CheckDomainExpiration(CheckRequest request)
    {

        try
        {
            var result = _domainExpirationChecker.Check(request.Uri);
            if (result.IsSuccessed)
                return OperationResult<DateTime>.Success(result.Data);

            return OperationResult<DateTime>.NotFound();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<OperationResult<bool>> CheckIpOrDomainCertificate(CheckRequest request)
    {

        try
        {
            var response = await _certificateChecker.CheckAsync(request.Uri);
            _logger.LogInformation("CheckIpOrDomainCertificate {0} ", response.RequestUri);
            return OperationResult<bool>.Success(response.Expired);
        }
        catch (CheckIpOrDomainCertificateException e)
        {
            return OperationResult<bool>.Error(e.Message);

        }
    }

    public async Task<OperationResult<GetResponseTimeDto>> GetRequestResponse(string ip, int timeout, Core.Enums.HttpMethod method)
    {

        try
        {


            HttpClient client = new();
            client.BaseAddress = new Uri(ip);
            client.Timeout = TimeSpan.FromMinutes(timeout);
            Stopwatch timer = new Stopwatch();
            timer.Start();
            var response = await client.SendAsync(new HttpRequestMessage(ConvertToHttpMethod(method), client.BaseAddress));
            response.EnsureSuccessStatusCode();

          
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            var GetResponse = new GetResponseTimeDto()
            {
                ResponseTime = timer.ElapsedMilliseconds,
                IsSuccessResponse = response.IsSuccessStatusCode,
                StatusCode=response.StatusCode,
                Reason=response.ReasonPhrase
            };
            client.Dispose();
            return OperationResult<GetResponseTimeDto>.Success(GetResponse);
        }
        catch (BaseApplicationExceptions e)
        {
            return OperationResult<GetResponseTimeDto>.Error(e.Message);
        }
    }

    public static HttpMethod ConvertToHttpMethod(Core.Enums.HttpMethod method) =>
         method switch
         {
             Core.Enums.HttpMethod.Get => HttpMethod.Get,
             Core.Enums.HttpMethod.Post => HttpMethod.Post,
             Core.Enums.HttpMethod.Put => HttpMethod.Put,
             Core.Enums.HttpMethod.Delete => HttpMethod.Delete,
             Core.Enums.HttpMethod.Head => HttpMethod.Head,
             Core.Enums.HttpMethod.Options => HttpMethod.Options,
             Core.Enums.HttpMethod.Patch => HttpMethod.Patch,
             _ => throw new ArgumentException("Unknown HttpMethod")
         };
}


