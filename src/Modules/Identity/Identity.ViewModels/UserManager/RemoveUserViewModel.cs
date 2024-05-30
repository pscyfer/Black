using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels.UserManager
{
    public class RemoveUserViewModel
    {
        [Required]
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }
}
