using Common.Application;
using Common.Application.DataTableConfig;
using TicketModule.Services.DTOs.Command.Ticket;
using TicketModule.Services.DTOs.Command.TicketMessage;
using TicketModule.Services.DTOs.Query;
using TicketModule.ViewModel;

namespace TicketModule.Services
{
    public interface ITicketService
    {
        Task<PaginationDataTableResult<TicketViewModel>> GetTicketPaggingAsync(FiltersFromRequestDataTableBase request,
            CancellationToken cancellationToken);
        Task<OperationResult<GetDetaileTicketQueryDto>> GetTicketWithMessagesQueryAsync(Guid ticketId, CancellationToken cancellationToken);
        Task<OperationResult> CreateTicketAsync(CreateTicketCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> CreateTicketMessageAsync(CreateClientTicketMessageCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> CreateTicketMessageAsync(CreateOperatorTicketMessageCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> UpdateTicketAsync(UpdateTicketCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> DeleteTicketAsync(RequestQueryById request);
        Task<OperationResult<TicketViewModel>> GetTicketViewModel(RequestQueryById requestQueryById);
        Task<OperationResult<TicketViewModel>> GetForRemoveAsync(RequestQueryById requestQueryById);

        #region TicketMessages

        Task<OperationResult> AddMessageToTicket(CreateOperatorTicketMessageCommandDto command);
        Task<OperationResult> AddMessageToTicket(CreateClientTicketMessageCommandDto command);

        #endregion

        Task<OperationResult> CloseTicket(RequestQueryById request);
    }
}
