using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.Authentication
{
    public class LoginVM
    {
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "شماره موبایل"), Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور"), Required(ErrorMessage = " لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "بخاطر بسپارم؟")]
        public bool IsRemember { get; set; }
    }
}
