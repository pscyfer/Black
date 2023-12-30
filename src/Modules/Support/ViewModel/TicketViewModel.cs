using Common.Application.Validation;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using TicketModule.Entities;

namespace TicketModule.ViewModel
{
    public class TicketViewModel
    {
        public Guid? Id { get; set; }
        [Display(Name = "عنوان", Prompt = "عنوان"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]

        public string Title { get; set; }
        public string State { get; set; }
        public Guid UserId { get; set; }
    }
}
