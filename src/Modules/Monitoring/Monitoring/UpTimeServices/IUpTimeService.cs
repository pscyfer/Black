using Common.Application;

namespace Monitoring.UpTimeServices;



/// <summary>
/// این اینترفس برای عملیات های رکوئست های سرکشی ها است
/// </summary>
public interface IUpTimeService
{
    Task<OperationResult> SendRequest(long monitorId);
   
}