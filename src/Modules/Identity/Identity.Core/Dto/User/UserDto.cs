namespace Identity.Core.Dto.User
{
    public class UserDto
    {
        public UserDto()
        {

        }

        public UserDto(Guid userId, string firstName, string lastName, string gender, string avatar)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Avatar = avatar;
        }

        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }


    }
}
