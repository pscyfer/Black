using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;

namespace Identity.ViewModels.UserManager;

public class RequestCreateUserViewModel
{
    [Display(Name = "نام کاربری", Prompt = "نام کاربری"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string UserName { get; set; }

    [Display(Name = "نام", Prompt = "نام "), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string FirstName { get; set; }

    [Display(Name = "فامیلی", Prompt = "فامیلی "), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string LastName { get; set; }
    [Display(Name = "رمزعبور", Prompt = "رمزعبور "), DataType(DataType.Password), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string Password { get; set; }

    [Display(Name = "تکرار رمزعبور", Prompt = "تکرار رمزعبور"), Compare(nameof(Password), ErrorMessage = "رمز عبور با تکرار رمز عبور مقایرت دارد"), DataType(DataType.Password), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
    public string ConfirmPassword { get; set; }
}