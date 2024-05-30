using System.ComponentModel.DataAnnotations;
using Common.Application.Validation;
using Common.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Identity.ViewModels.UserManager
{
    public class UserEditViewModel
    {
        public UserEditViewModel(Guid userId, string userName, string firstName, string lastName, string email, string phoneNumber, GenderType gender, string avatar,  bool isActive, string birthDay)
        {
            UserId = userId;
            UserName = userName;
            FirstName = firstName;
            Email = email;
            LastName = lastName;
            Gender = gender;
            Avatar = avatar;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
            BirthDay = birthDay;

        }
        public UserEditViewModel()
        {

        }
        public Guid UserId { get; set; }
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
        public string Avatar { get; set; }

        [Display(Name = "شماره موبایل", Prompt = "شماره موبایل"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
        public string PhoneNumber { get; set; }

        [Display(Name = "فعال یا غیرفعال", Prompt = "فعال یا غیرفعال"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ تولد", Prompt = "تاریخ تولد"), Required(ErrorMessage = DataAnnotationMessages.RequiredMessage)]
        public string BirthDay { get; set; }

        public IFormFile? UserAvatarFile { get; set; }
        
    }

}

