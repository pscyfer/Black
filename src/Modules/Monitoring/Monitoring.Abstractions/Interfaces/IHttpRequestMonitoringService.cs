using Common.Application;
using Common.Application.DataTableConfig;
using Monitoring.Abstractions.DTOs.http;
using Monitoring.Abstractions.ViewModels;

namespace Monitoring.Abstractions.Interfaces;

public interface IHttpRequestMonitoringService
{
    Task<PaginationDataTableResult<GetHttpRequestMonitoringPagginDto>> GetPagingAsync(FiltersFromRequestDataTableBase request, CustomsFilterPagingDto customsFilter);
    Task<OperationResult<long>> CreateAsync(CreateHttpRequestCommandDto command);
    Task<OperationResult<GetForRemoveHttpRequestQueryDto>> GetForDeleteAsync(RequestQueryById<long> request);
    Task<OperationResult<GetHttpRequestMonitoringDto>> GetBy(RequestQueryById<long> request);
    Task<OperationResult> DeleteAsync(RequestQueryById<long> request);
    Task<bool> IsExist(RequestQueryById<long> request);
    Task<OperationResult> UpdateExpireDomainDate(DateTime dateTime, long monitorId);
    Task<OperationResult<GetMonitorNamebyIdViewModel>> GetMonitorName(long id);
    Task<OperationResult> ChangeIsPasuedMonitor(long id, bool isPausedValue);
    Task<OperationResult<EditHttpMonitorViewModel>> GetForEditAsync(RequestQueryById<long> request);
    Task<OperationResult> EditAsync(EditHttpMonitorViewModel request,string fullName);
}