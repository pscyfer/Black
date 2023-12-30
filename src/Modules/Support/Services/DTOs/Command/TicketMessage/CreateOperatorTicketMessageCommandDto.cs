using TicketModule.Entities;

namespace TicketModule.Services.DTOs.Command.TicketMessage
{
    public record CreateOperatorTicketMessageCommandDto( Guid UserId, string Message, string FileAttachment, Guid TicketId, string FullName);
}
