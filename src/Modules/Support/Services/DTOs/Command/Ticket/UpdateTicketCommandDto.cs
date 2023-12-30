namespace TicketModule.Services.DTOs.Command.Ticket;

public record UpdateTicketCommandDto(Guid Id,string Title,  Guid UserCreatedId,string FullName);