using Common.Domain.Enums;

namespace Identity.Core.Dto.User
{
    public class GetUserForUpdateDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderType Gender { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; } = false;
        public string BirthDay { get; set; }
        public string Email { get; set; }
    }
}
