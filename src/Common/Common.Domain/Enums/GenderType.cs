using System.ComponentModel.DataAnnotations;
namespace Common.Domain.Enums;

public enum GenderType : sbyte
{
    [Display(Name = "زن")]
    Female = 0,
    [Display(Name = "مرد")]
    Male = 1,
    [Display(Name = "نامشخص")]
    Unknown = 2,
}