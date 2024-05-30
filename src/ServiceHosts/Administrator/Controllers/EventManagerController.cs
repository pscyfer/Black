using Administrator.Infrastructure.DataTableHelper;
using Common.Application;
using Common.Application.DataTableConfig;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Abstractions.DTOs.Event;
using Monitoring.Abstractions.Interfaces;

namespace Administrator.Controllers
{
    public class EventManagerController : BaseController
    {
        private readonly IEventMonitoringService _eventService;

        public EventManagerController(IEventMonitoringService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index(long? monitorId)
        {
            return View(InitTableView.EventMonitoringDataTable());
        }

        #region CRUD
        [HttpPost]
        public async Task<IActionResult> GetListEventPaging()
        {
            Request.GetDataFromRequest(out FiltersFromRequestDataTableBase filtersFromRequest);
            var result = await _eventService.GetPagingAsync(filtersFromRequest, new CustomerEventFilterPagingDto(null));
            return new JsonResult(result);
        }
        #endregion
    }
}
