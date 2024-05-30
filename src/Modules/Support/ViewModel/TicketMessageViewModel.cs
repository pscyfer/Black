using Microsoft.AspNetCore.Http;
using TicketModule.Entities;

namespace TicketModule.ViewModel;

public class TicketMessageViewModel
{
    public Guid TicketId { get; set; }
    public string Message { get; set; }

    public string FileAttachmentAddress { get; set; }

    public IFormFile File { get; set; }

}