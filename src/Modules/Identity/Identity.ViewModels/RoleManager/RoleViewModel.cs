using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Identity.ViewModels.RoleManager;

public class RoleVm
{
    public Guid? RoleId { get; set; }

    [Display(Name = "نام نقش", Prompt = "نام نقش"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Name { get; set; }

    [Display(Name = "توضیحات نقش", Prompt = "توضیحات نقش"),
     Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Description { get; set; }

   
}