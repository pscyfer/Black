using System.ComponentModel.DataAnnotations;

namespace TicketModule.Entities
{
    public enum TicketState : byte
    {
        [Display(Name = "باز")]
        Open,
        [Display(Name = "درحال بررسی")]
        Pending,
        [Display(Name = "پاسخ داده شده")]
        Answered,
        [Display(Name = "بسته")]
        Closed
    }
}
