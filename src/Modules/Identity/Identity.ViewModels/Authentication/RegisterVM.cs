using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Authentication
{
    public class RegisterVM
    {
        [Display(Name = "شماره موبایل"), Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه عبور"), Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "تکرار کلمه عبور"), Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        [Compare("Password", ErrorMessage = "تکرار کلمه عبور با کلمه عبور  یکی نمی باشد.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
