using TicketModule.Entities;

namespace TicketModule.Services.DTOs.Query
{
    public class GetDetaileTicketQueryDto
    {
        public Guid TicketId { get; set; }
        public string TicketTitle { get; set; }
        public TicketState State { get; set; }

        public List<GetTicketMessageQueryDto> Messages { get; set; }
    }

}
