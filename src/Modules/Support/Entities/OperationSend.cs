using System.ComponentModel.DataAnnotations;

namespace TicketModule.Entities
{
    public enum OperationSend:sbyte
    {
        [Display(Name ="کاربر")]
        Client=0,
        [Display(Name = "اپراتور")]
        Operator = 1
    }
}
