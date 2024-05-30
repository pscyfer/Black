using Common.Domain;

namespace TicketModule.Entities
{
    public class TicketMessage : BaseEntity
    {

        public string Message { get; set; }

        /// <summary>
        /// 0= client,
        /// 1=admin
        /// </summary>
        public OperationSend OperationSend { get; set; }
        public string FileAttachment { get; set; }
        public Guid TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }


        public string UserFullName { get; set; }
        public Guid UserId { get; set; }
    }
}
