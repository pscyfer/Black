using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.ViewModels.RoleManager;

public class GetRoleWithPermissionsViewModel
{
    public GetRoleWithPermissionsViewModel()
    {
        PermissionNames = Array.Empty<string>();
    }

    public GetRoleWithPermissionsViewModel(Guid roleId, string name, string desciption)
    {
        RoleId = roleId;
        Name = name;
        PermissionNames = Array.Empty<string>();
        Desciption = desciption;
    }

    public Guid RoleId { get; set; }

    [Display(Name = "نام نقش", Prompt = "نام نقش"),
     Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]

    public string Name { get; set; }

    [Display(Name = "توضیحات نقش", Prompt = "توضیحات نقش")]
    [Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Desciption { get; set; }

    public string[] PermissionNames { get; set; }

    /// <summary>
    /// لیست دسترسی ها به صورت لیست آبشاری
    /// </summary>
    public IEnumerable<SelectListItem> Permissions { get; set; }
}