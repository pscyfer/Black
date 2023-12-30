using TicketModule.Entities;

namespace TicketModule.Services.DTOs.Query
{
    public class GetTicketMessageQueryDto
    {
        public string Message { get; set; }
        public OperationSend OperationSend { get; set; }
        public string FileAttachment { get; set; }
        public DateTime SendDate { get; set; }
        public Guid UserId { get; set; }
    }

}
