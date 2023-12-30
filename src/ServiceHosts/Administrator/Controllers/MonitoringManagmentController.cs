using Administrator.Infrastructure.DataTableHelper;
using Administrator.Infrastructure.Routeing;
using Administrator.Infrastructure.SideBarMenu;
using Azure;
using Common.Application;
using Common.Application.DataTableConfig;
using Common.AspNetCore;
using Common.AspNetCore.Notification;
using Hangfire;
using Identity.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Monitoring.Abstractions.DTOs.Event;
using Monitoring.Abstractions.DTOs.http;
using Monitoring.Abstractions.Interfaces;
using Monitoring.Abstractions.ViewModels;
using Monitoring.EF_Services;
using Monitoring.UpTimeServices;

namespace Administrator.Controllers
{
    public class MonitoringManagementController : BaseController
    {
        #region Fields
        private readonly IHttpRequestMonitoringService _httpRequestMonitoring;
        private readonly INotification _notification;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IUpTimeService _upTimeService;
        private readonly IEventMonitoringService _eventMonitoringService;
        #endregion
        #region Ctor
        public MonitoringManagementController(IHttpRequestMonitoringService httpRequestMonitoring, INotification notification, IBackgroundJobClient backgroundJobClient, IUpTimeService upTimeService, IEventMonitoringService eventMonitoringService)
        {
            _httpRequestMonitoring = httpRequestMonitoring;
            _notification = notification;
            _backgroundJobClient = backgroundJobClient;
            _upTimeService = upTimeService;
            _eventMonitoringService = eventMonitoringService;
        }

        #endregion

        public IActionResult Index()
        {
            TempData["IsPauseMonitorSelectListKey"] = new List<SelectListItem>()
            {
            new SelectListItem("فعال","True"),
            new SelectListItem("معلق","False")
            };

            ActiveMenu(SideBarMenuActivator.MonitoringManagement_https);
            return View(InitTableView.MonitoringHttpDataTable());
        }

        #region CRUD-Http

        [HttpGet, Route(BaseRouteing.RouteChangeIsPause)]
        public async Task<IActionResult> ChangeIsPause(long id, bool state)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) return BadRequest();

            var result = await _httpRequestMonitoring.GetMonitorName(id);
            if (!result.IsSuccessed) return BadRequest(result.Message);
            result.Data.IsPausedValue = state;
            return PartialView("_RenderChangeIsPause", result.Data);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmIsPauseMonitor(GetMonitorNamebyIdViewModel model)
        {
            if (!ModelState.IsValid) return PartialView("_RenderChangeIsPause", model);
            var result = await _httpRequestMonitoring.ChangeIsPasuedMonitor(model.Id, model.IsPausedValue);
            if (!result.IsSuccessed)
                SetAjaxNotification(OperationMessages.Warnning);

            SetAjaxNotification(OperationMessages.OperationSuccess);
            return PartialView("_RenderChangeIsPause", model);
        }
        public IActionResult CreateHttp()
        {
            ActiveMenu(SideBarMenuActivator.MonitoringManagement_https);
            var viewModel = new CreateHttpMonitorViewModel();
            return View(viewModel);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHttp(CreateHttpMonitorViewModel model)
        {
            ActiveMenu(SideBarMenuActivator.MonitoringManagement_https);
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _httpRequestMonitoring
                .CreateAsync(new CreateHttpRequestCommandDto(GetCurrnetUserId, AuthHelper.GetFullName(User),
                model.Ip, model.Name, model.Interval, model.Timeout, model.IsSslVerification, model.IsDoaminCheck));

            await _eventMonitoringService.AddFirstEvent(new CreateFirstEventCommandDto(result.Data));

            if (!result.IsSuccessed)
            {

                _notification.ErrorNotify(result.Message);
                return View(model);

            }
            _notification.SuccessNotify(OperationMessages.OperationSuccess);
            var manager = new RecurringJobManager();
            manager.AddOrUpdate<IUpTimeService>(Guid.NewGuid().ToString(),
                x => x.SendRequest(result.Data),
                $"*/{model.Interval} * * * *");

            return RedirectToAction("Index");
        }

        [HttpGet, Route(BaseRouteing.RouteDefaultMonitorIdentifier)]
        public async Task<IActionResult> EditHttp(RequestQueryById<long> request)
        {
            ActiveMenu(SideBarMenuActivator.MonitoringManagement_https);
            if (!ModelState.IsValid) return NotFound();

            var find = await _httpRequestMonitoring.GetForEditAsync(request);
            if (find.IsSuccessed)
            {
                return View(find.Data);
            }
            return NotFound();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHttp(EditHttpMonitorViewModel request)
        {
            ActiveMenu(SideBarMenuActivator.MonitoringManagement_https);
            if (!ModelState.IsValid) return NotFound();

            var result = await _httpRequestMonitoring.EditAsync(request, AuthHelper.GetFullName(User));
            if (!result.IsSuccessed)
                _notification.ErrorNotify(result.Message);

            _notification.SuccessNotify(OperationMessages.OperationSuccess);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetListHttpPaging()
        {
            bool? IsPause = null;
            var filterIsPause = Request.Form["FilterIsPause"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(filterIsPause))
                IsPause = Convert.ToBoolean(filterIsPause);

            Request.GetDataFromRequest(out FiltersFromRequestDataTableBase filtersFromRequest);
            var result = await _httpRequestMonitoring.GetPagingAsync(filtersFromRequest,
                new CustomsFilterPagingDto(IsPause));
            return new JsonResult(result);
        }

        #endregion

    }
}
