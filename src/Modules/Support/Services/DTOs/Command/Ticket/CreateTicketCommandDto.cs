namespace TicketModule.Services.DTOs.Command.Ticket
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Title"></param>
    /// <param name="UserCreatedId"></param>
    public record CreateTicketCommandDto(string Title,  Guid UserCreatedId,string FullName);
}
