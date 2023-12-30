using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Monitoring.Abstractions.ViewModels;

public class CreateHttpMonitorViewModel
{
    [Display(Name = "آدرس یا Ip ", Prompt = "آدرس یا Ip"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Ip { get; set; }
    [Display(Name = "نام", Prompt = "نام"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Name { get; set; }
    [Display(Name = "مدت زمان درخواست", Prompt = "مدت زمان درخواست"), Range(3, 30, ErrorMessage = "زمان درخواست باید بین 3 تا 30 دقیقه باشد"),
     Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]

    public int Interval { get; set; }

    [Display(Name = "زمان منتظر نتیجه", Prompt = "زمان منتظر نتیجه"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public int Timeout { get; set; }

    [Display(Name = "SSL چک شود", Prompt = "SSL چک شود"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public bool IsSslVerification { get; set; }

    [Display(Name = "تاریخ انقضای دامنه چک شود", Prompt = "تاریخ انقضای دامنه چک شود"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public bool IsDoaminCheck { get; set; }
}
