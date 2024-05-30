using Common.Application;
using Common.Application.DataTableConfig;
using Monitoring.Abstractions.DTOs.Event;

namespace Monitoring.Abstractions.Interfaces
{
    public interface IEventMonitoringService
    {
        Task<PaginationDataTableResult<GetEventListDto>> GetPagingAsync(FiltersFromRequestDataTableBase request, CustomerEventFilterPagingDto customerFilter);
        Task<OperationResult> AddEvent(CreateEventCommandDto command);
        Task<OperationResult> AddFirstEvent(CreateFirstEventCommandDto command);
        Task<OperationResult<string>> CalculateDurationEvent(RequestQueryById<long> command);

    }
}
