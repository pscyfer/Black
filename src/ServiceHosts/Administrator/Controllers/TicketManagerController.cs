using System.ComponentModel;
using Administrator.Infrastructure.DataTableHelper;
using Administrator.Infrastructure.Routeing;
using Common.Application;
using Common.Application.DataTableConfig;
using Common.AspNetCore;
using Common.AspNetCore.Autorizetion.DynamicAuthorizationService.Enum;
using Common.AspNetCore.Extensions;
using Common.AspNetCore.LocalFileProvider;
using Common.Domain.Exceptions;
using Identity.Core;
using Identity.Core.Dto.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketModule;
using TicketModule.Services;
using TicketModule.Services.DTOs.Command.Ticket;
using TicketModule.Services.DTOs.Command.TicketMessage;
using TicketModule.ViewModel;

namespace Administrator.Controllers
{
    [Authorize(Policy = ConstantPolicies.RequireAdministratorRole)]
    [DisplayName("مدیریت تیکت ها")]
    public class TicketManagerController : BaseController
    {
        private readonly ITicketService _ticketService;
        private readonly IFileManager _fileManager;
        public TicketManagerController(ITicketService ticketService, IFileManager fileManager)
        {
            _ticketService = ticketService;
            _fileManager = fileManager;
        }

        public IActionResult Index()
        {
            ActiveMenu("TicketManager");
            return View(InitTableView.SupportTicketDataTable());
        }

        #region CRUD
        [HttpPost]
        public async Task<IActionResult> GetListPaging(CancellationToken cancellationToken)
        {
            Request.GetDataFromRequest(out FiltersFromRequestDataTableBase filtersFromRequest);
            var result = await _ticketService.GetTicketPaggingAsync(filtersFromRequest, cancellationToken);
            return new JsonResult(result);
        }

        [Route(BaseRouteing.RouteDefaultTicketId), HttpGet]
        public async Task<IActionResult> CloseTicket(Guid id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString())) return PartialView("_RenderCloseTicket");

            var closeTicketResult = await _ticketService.GetTicketViewModel(new RequestQueryById(id));
            if (closeTicketResult.IsSuccessed)
            {
                return PartialView("_RenderCloseTicket", closeTicketResult.Data);
            }
            return PartialView("_RenderCloseTicket");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseTicket(TicketViewModel model)
        {
            ModelState.Remove("Title");
            ModelState.Remove("State");
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                var result = await _ticketService.CloseTicket(new RequestQueryById(model.Id.Value));
                if (result.IsSuccessed)
                {
                    SetAjaxNotification(OperationMessages.OperationSuccess);
                    return PartialView("_RenderCloseTicket", model);
                }
                SetAjaxNotification(OperationMessages.Warnning);
            }
            return PartialView("_RenderCloseTicket", model);

        }

        public async Task<IActionResult> RenderTicket(Guid? id)
        {
            var model = new TicketViewModel();
            if (id.HasValue)
            {
                var result = await _ticketService.GetTicketViewModel(new RequestQueryById(id.Value));
                if (result is { IsSuccessed: true, Data: not null })
                {
                    model = result.Data;
                }
            }

            return PartialView("_RenderCreateUpdate", model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrUpdate(TicketViewModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != null)
                {
                    await _ticketService.UpdateTicketAsync(new UpdateTicketCommandDto(model.Id.Value, model.Title, GetCurrnetUserId, AuthHelper.GetFullName(User)), cancellationToken);
                }
                else
                {
                    await _ticketService.CreateTicketAsync(new CreateTicketCommandDto(model.Title, GetCurrnetUserId, AuthHelper.GetFullName(User)), cancellationToken);
                }
            }

            SetAjaxNotification(OperationMessages.OperationSuccess);
            return PartialView("_RenderCreateUpdate", model);
        }

        [Route(BaseRouteing.RouteDefaultTicketId), HttpGet]
        public async Task<IActionResult> RenderRemove(Guid id)
        {
            var ticket = await _ticketService.GetForRemoveAsync(new RequestQueryById(id));
            if (ticket.IsSuccessed)
                return PartialView("_Delete", ticket.Data);
            throw new BaseDomainException("ticket Not Found");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(TicketViewModel model)
        {
            ModelState.Remove("Title");
            if (!ModelState.IsValid) return PartialView("_Delete", model);

            var result = await _ticketService.DeleteTicketAsync(new RequestQueryById(model.Id.Value));
            if (result.IsSuccessed)
                TempData["notification"] = OperationMessages.OperationSuccess;
            return PartialView("_Delete", model);
        }
        #endregion

        [Route(BaseRouteing.RouteDefaultTicketId)]
        public async Task<IActionResult> Detaile(Guid id, CancellationToken cancellation)
        {
            ActiveMenu("TicketManager");
            if (string.IsNullOrWhiteSpace(id.ToString())) return NotFound();

            var getMessages = await _ticketService.GetTicketWithMessagesQueryAsync(id, cancellation);


            if (getMessages.IsSuccessed)
            {
                return View(getMessages.Data);

            }
            return NotFound();
        }

        [HttpPost]
        public async Task<JsonResult> SendNewMessage(TicketMessageViewModel model, CancellationToken cancellationToken)
        {
            try
            {
                string fileName = string.Empty;
                if (model.File != null && model.File.Length > 0)
                {
                    fileName = await _fileManager.CreateFileAtPathAsync(TicketDirectory.FileAttachment, model.File);
                }
                var result = await _ticketService.CreateTicketMessageAsync(new
                    CreateOperatorTicketMessageCommandDto(GetCurrnetUserId, model.Message,
                        fileName, model.TicketId, AuthHelper.GetFullName(User)), cancellationToken);
                return new JsonResult(result);

            }
            catch (BaseAspNetCoreExceptions e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
