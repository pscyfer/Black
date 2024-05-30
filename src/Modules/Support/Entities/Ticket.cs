using Common.Domain;

namespace TicketModule.Entities
{
    public class Ticket : BaseEntity
    {
        public string Title { get; set; }
        public string OwnerFullName { get; set; }   
        public TicketState State { get; set; }
        public Guid UserId { get; set; }

        public virtual ICollection<TicketMessage> Messages { get; set; }
    }
}
