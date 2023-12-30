using Common.Application;
using Common.Application.DataTableConfig;
using Common.Application.Exceptions;
using Common.AspNetCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Monitoring.Abstractions.DTOs.http;
using Monitoring.Abstractions.Interfaces;
using Monitoring.Abstractions.ViewModels;
using Monitoring.Core;
using Monitoring.Core.Entities.ValueObjects;
using System.Linq.Dynamic.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Monitor = Monitoring.Core.Entities.Monitor;

namespace Monitoring.EF_Services;

public class HttpRequestMonitoringService : IHttpRequestMonitoringService
{
    private readonly MonitorDbContext _dbContext;
    private readonly ILogger<HttpRequestMonitoringService> _logger;
    public HttpRequestMonitoringService(MonitorDbContext dbContext, ILogger<HttpRequestMonitoringService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }


    public virtual async Task<PaginationDataTableResult<GetHttpRequestMonitoringPagginDto>> GetPagingAsync(FiltersFromRequestDataTableBase request, CustomsFilterPagingDto customsFilter)
    {
        try
        {
            List<GetHttpRequestMonitoringPagginDto> MonitorList = new();

            var query = _dbContext.Monitors.AsNoTracking().AsQueryable();
            if (!(string.IsNullOrEmpty(request.sortColumn) && string.IsNullOrEmpty(request.sortColumnDirection)))
            {
                query = query
                .OrderBy(request.sortColumn + " " + request.sortColumnDirection);
            }

            if (!string.IsNullOrEmpty(request.searchValue))
            {
                query = query.Where(x =>
                x.Name.Contains(request.searchValue) || x.Ip.Contains(request.searchValue));
            }

            if (customsFilter.IsPause.HasValue)
            {
                query = query
                .Where(x => x.IsPause == customsFilter.IsPause);
            }

            int recordsFiltered = await query.CountAsync();
            MonitorList = await query.Skip(request.skip).Take(request.pageSize).Select(x=> new GetHttpRequestMonitoringPagginDto
            {
                HttpMethod=x.Http.Method.ToDisplay(DisplayProperty.Name),
                Id=x.Id,
                Interval=x.Interval,
                Ip=x.Ip,
                IsPause = x.IsPause,
                Name = x.Name,
                OwnerFullName = x.OwnerFullName,
                Timeout = x.Timeout,
                UpTimeFor = x.UpTimeFor,
                UserId = x.UserId,
                
            }).ToListAsync();

            int recordsTotal = await _dbContext.Monitors.AsNoTracking().CountAsync();

            PaggingDataTableExtention.ConfigPaging(ref request, recordsTotal);

            var result = new PaginationDataTableResult<GetHttpRequestMonitoringPagginDto>()
            {
                draw = request.draw,
                recordsFiltered = recordsFiltered,
                recordsTotal = recordsTotal,
                data = MonitorList
            };
            return result;


        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<OperationResult<long>> CreateAsync(CreateHttpRequestCommandDto command)
    {
        try
        {
            var monitor = Monitor.Intance();
            monitor.CreateDefaultValue(command.UserId, command.OwnerFullName, command.Ip, command.Name, command.Interval, command.Timeout);
            var httpRequest = new HttpRequest()
            {
                IsDomainCheck = command.IsDomainCheck,
                IsSslVerification = command.IsSslVerification,
            };
            monitor.CreateHttpRequest(httpRequest);

            await _dbContext.Monitors.AddAsync(monitor);

            await _dbContext.SaveChangesAsync();

            return OperationResult<long>.Success(monitor.Id);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }



    public virtual async Task<OperationResult<GetForRemoveHttpRequestQueryDto>> GetForDeleteAsync(RequestQueryById<long> request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult<GetForRemoveHttpRequestQueryDto>.NotFound();

            var monitor = await _dbContext.Monitors.AsNoTracking().Where(x => x.Id == request.Identifier)
                .Select(x => new GetForRemoveHttpRequestQueryDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).FirstOrDefaultAsync();

            if (monitor is null) return OperationResult<GetForRemoveHttpRequestQueryDto>.NotFound();

            return OperationResult<GetForRemoveHttpRequestQueryDto>.Success(monitor);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<OperationResult<GetHttpRequestMonitoringDto>> GetBy(RequestQueryById<long> request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult<GetHttpRequestMonitoringDto>.NotFound();

            var monitor = await _dbContext.Monitors.AsTracking()
                .Where(x => x.Id == request.Identifier && !x.IsPause)
                .Select(x => new GetHttpRequestMonitoringDto
                {
                    Id = x.Id,
                    Ip = x.Ip,
                    Name = x.Name,
                    Interval = x.Interval,
                    Timeout = x.Timeout,
                    IsPause = x.IsPause,
                    LastChecked = x.LastChecked,
                    UpTimeFor = x.UpTimeFor,
                    StatusCode = (int)x.Http.StatusCode,
                    IsSslVerification = x.Http.IsSslVerification,
                    IsDoaminCheck = x.Http.IsDomainCheck,
                    HttpMethod = (int)x.Http.Method,
                    UserId = x.UserId,
                })
                .FirstOrDefaultAsync();
            return monitor is null ? OperationResult<GetHttpRequestMonitoringDto>.NotFound() : OperationResult<GetHttpRequestMonitoringDto>.Success(monitor);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public virtual async Task<OperationResult> DeleteAsync(RequestQueryById<long> request)
    {
        try
        {
            if (!await IsExist(request)) return OperationResult.NotFound();
            var monitor = await _dbContext.Monitors.FindAsync(request.Identifier);
            if (monitor is null) return OperationResult.NotFound();
            _dbContext.Monitors.Remove(monitor);

            await _dbContext.SaveChangesAsync();

            return OperationResult.Success();
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> IsExist(RequestQueryById<long> request)
    {
        try
        {
            return await _dbContext.Monitors.AsNoTracking().AnyAsync(x => x.Id == request.Identifier);
        }
        catch (BaseApplicationExceptions e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<OperationResult> UpdateExpireDomainDate(DateTime dateTime, long monitorId)
    {

        try
        {
            if (!await IsExist(new RequestQueryById<long>(monitorId))) return OperationResult.NotFound();

            var monitor = await _dbContext.Monitors.FindAsync(monitorId);
            monitor.SetDominExpierDate(dateTime);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Add ExpierDate For Monitor Id ={0} and ExpierDateAt={1}", monitor.Id, dateTime);

            return OperationResult.Success();
        }
        catch (BaseApplicationExceptions e)
        {
            _logger.LogError(e.Message);
            throw;
        }

    }

    public async Task<OperationResult<GetMonitorNamebyIdViewModel>> GetMonitorName(long id)
    {
        try
        {
            if (!await IsExist(new RequestQueryById<long>(id))) return OperationResult<GetMonitorNamebyIdViewModel>.NotFound();


            var findmonitor = await _dbContext.Monitors
                .Where(x => x.Id == id)
                .Select(x => new { x.Name, x.Id, x.IsPause })
                .FirstOrDefaultAsync();
            if (findmonitor == null) return OperationResult<GetMonitorNamebyIdViewModel>.NotFound();
            return OperationResult<GetMonitorNamebyIdViewModel>.Success(new GetMonitorNamebyIdViewModel(findmonitor.Name, findmonitor.Id, findmonitor.IsPause));
        }
        catch (BaseApplicationExceptions)
        {
            throw;
        }
    }

    public async Task<OperationResult> ChangeIsPasuedMonitor(long id, bool isPausedValue)
    {
        try
        {
            if (!await IsExist(new RequestQueryById<long>(id))) return OperationResult.NotFound();


            var monitor = await _dbContext.Monitors.FindAsync(id);
            monitor.IsPause = isPausedValue;

            _dbContext.Monitors.Update(monitor);
            await _dbContext.SaveChangesAsync();

            return OperationResult.Success();
        }
        catch (BaseApplicationExceptions)
        {

            throw;
        }
    }

    public async Task<OperationResult<EditHttpMonitorViewModel>> GetForEditAsync(RequestQueryById<long> request)
    {
        try
        {
            if (!await IsExist(request))
                return OperationResult<EditHttpMonitorViewModel>.NotFound();

            var mointor = await _dbContext.Monitors.AsNoTracking()
                .Where(x => x.Id == request.Identifier)
                .Select(x => new EditHttpMonitorViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Interval = x.Interval,
                    Ip = x.Ip,
                    IsDoaminCheck = x.Http.IsDomainCheck,
                    IsSslVerification = x.Http.IsSslVerification,
                    Timeout = x.Timeout,
                    IsPause = x.IsPause
                }).FirstOrDefaultAsync();

            if (mointor == null)
                return OperationResult<EditHttpMonitorViewModel>.NotFound();

            return OperationResult<EditHttpMonitorViewModel>.Success(mointor);
        }
        catch (BaseApplicationExceptions)
        {
            throw;
        }
    }

    public async Task<OperationResult> EditAsync(EditHttpMonitorViewModel request, string fullName)
    {
        try
        {
            if (!await IsExist(new RequestQueryById<long>(request.Id)))
                return OperationResult.NotFound();

            var monitor = await _dbContext.Monitors.FindAsync(request.Id);
            monitor.EditHttpRequest(fullName, request.Name, request.Ip, request.Interval, request.Timeout,
                request.IsSslVerification, request.IsDoaminCheck, request.IsPause);

            _dbContext.Monitors.Update(monitor);

            await _dbContext.SaveChangesAsync();

            return OperationResult.Success();
        }
        catch (BaseApplicationExceptions)
        {

            throw;
        }
    }
}