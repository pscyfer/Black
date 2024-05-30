using Common.Application;
using Common.Application.DataTableConfig;
using Common.Application.DateUtil;
using Common.Application.Exceptions;
using Common.AspNetCore.Extensions;
using Microsoft.EntityFrameworkCore;
using Monitoring.Abstractions.DTOs.Event;
using Monitoring.Abstractions.DTOs.http;
using Monitoring.Abstractions.Interfaces;
using Monitoring.Core;
using Monitoring.Core.Entities;
using System.Linq.Dynamic.Core;
namespace Monitoring.EF_Services
{
    public class EventMonitoringService : IEventMonitoringService
    {
        private readonly MonitorDbContext _context;

        public EventMonitoringService(MonitorDbContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> AddEvent(CreateEventCommandDto command)
        {
            try
            {
                var createEvent = Event.NewInstance();
                createEvent.Create(command.MonitorId, command.EventType, command.Reason);

                await _context.Events.AddAsync(createEvent);
                await _context.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions)
            {

                throw;
            }
        }
        public async Task<OperationResult> AddFirstEvent(CreateFirstEventCommandDto command)
        {
            try
            {
                var createEvent = Event.NewInstance();
                createEvent.Create(command.MonitorId, Core.Enums.EventType.Start, "Start");

                await _context.Events.AddAsync(createEvent);
                await _context.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (BaseApplicationExceptions)
            {

                throw;
            }
        }
        public async Task<OperationResult<string>> CalculateDurationEvent(RequestQueryById<long> command)
        {
            try
            {
                var query = await _context.Events.AsNoTracking()
                    .OrderByDescending(X => X.DateTime)
                    .FirstOrDefaultAsync(x => x.MonitorId == command.Identifier);
                if (query == null) return OperationResult<string>.Success("");

                string duration = DateConvertor.DifferenceTwoDateTime(DateTime.Now, query.DateTime);

                string result = duration.Replace("-", "");

                return OperationResult<string>.Success(result);


            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task<PaginationDataTableResult<GetEventListDto>> GetPagingAsync(FiltersFromRequestDataTableBase request, CustomerEventFilterPagingDto customerFilter)
        {
            try
            {

                var query = _context.Events.AsNoTracking().Include(x => x.Monitor).AsQueryable();

                if (!(string.IsNullOrEmpty(request.sortColumn) && string.IsNullOrEmpty(request.sortColumnDirection)))
                {
                    query = query.OrderBy(request.sortColumn + " " + request.sortColumnDirection);
                }

                if (!string.IsNullOrEmpty(request.searchValue))
                {
                    query = query.Where(x =>
                    x.Monitor.Name.Contains(request.searchValue) || x.Reason.Contains(request.searchValue));
                }
                if (customerFilter.MonitorId.HasValue)
                {
                    query = query.Where(x=>x.MonitorId==customerFilter.MonitorId);
                }
                var recordsFiltered = await query.CountAsync();
                var queryResult = await query.Skip(request.skip).Take(request.pageSize)
                    .Select(x => new
                    {
                        x.EventType,
                        x.Reason,
                        x.DateTime,
                        x.Id,
                        x.Monitor.Name
                    })
                    .ToListAsync();

                var listEvent = new List<GetEventListDto>();
                for (int i = 0; i < queryResult.Count; i++)
                {
                    string duration = string.Empty;
                    if (i > 1)
                    {
                        duration = DateConvertor.DifferenceTwoDateTime(DateTime.Now, queryResult[i - 1].DateTime);
                    }
                    var Event = new GetEventListDto()
                    {
                        DateTime = queryResult[i].DateTime.ToPersianDate(),
                        MonitorName = queryResult[i].Name,
                        Reason = queryResult[i].Reason,
                        TypeOfEvent = queryResult[i].EventType.ToDisplay(DisplayProperty.Name),
                        Duration = duration

                    };
                    listEvent.Add(Event);
                }

                var recordsTotal = await _context.Events.AsNoTracking().CountAsync();
                PaggingDataTableExtention.ConfigPaging(ref request, recordsTotal);
                var result = new PaginationDataTableResult<GetEventListDto>()
                {
                    draw = request.draw,
                    recordsFiltered = recordsFiltered,
                    recordsTotal = recordsTotal,
                    data = listEvent
                };
                return result;

            }
            catch (BaseApplicationExceptions)
            {

                throw;
            }
        }
    }
}
