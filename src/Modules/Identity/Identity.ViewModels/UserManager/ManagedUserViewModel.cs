using Common.Application.Validation;
using Common.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.UserManager;

public class ManagedUserViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "نام کاربری", Prompt = "نام کاربری"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string UserName { get; set; }

    [Display(Name = "نام", Prompt = "نام"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string FirstName { get; set; }
    [Display(Name = "ایمیل", Prompt = "@gmail.com"), EmailAddress(ErrorMessage = DataAnnotationMessages.RequiredEmailMessage), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Email { get; set; }

    [Display(Name = "نام خانوادگی", Prompt = "نام خانوادگی"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string LastName { get; set; }

    [Display(Name = "جنسیت", Prompt = "جنسیت"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public GenderType Gender { get; set; }

    [Display(Name = "شماره موبایل", Prompt = "شماره موبایل"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string PhoneNumber { get; set; }

    [Display(Name = "فعال یا غیرفعال", Prompt = "فعال یا غیرفعال"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public bool IsActive { get; set; }

    [Display(Name = "تاریخ تولد", Prompt = "تاریخ تولد"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string BirthDay { get; set; }
    [Display(Name = "کاربر حذف شده؟")]
    public bool IsDelete { get; set; }

    public bool EmailConfirmed { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public int AccessFailedCount { get; set; }

    public SocialNetworkViewModel SocialNetwork { get; set; }
    public IList<SelectListItem>? RolePermission { get; set; }
    public string[] SelectedRole { get; set; }
}