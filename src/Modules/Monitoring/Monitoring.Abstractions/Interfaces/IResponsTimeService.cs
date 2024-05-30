using Common.Application;
using Monitoring.Abstractions.DTOs.ResponseTime;

namespace Monitoring.Abstractions.Interfaces
{
    public interface IResponsTimeService
    {
        Task<OperationResult> CreateResponsTime(CreateResponsTimeDto command);
    }
}
