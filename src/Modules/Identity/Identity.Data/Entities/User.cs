using Common.Domain;
using Common.Domain.Enums;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Entities
{
    public sealed class User : IdentityUser<Guid>
    {
        public User()
        {
            Gender = GenderType.Unknown;
            LockoutEnabled = false;
            Avatar = "/modules/identity/default.png";
            SocialNetwork = new SocialNetwork();
            Id = SequentialGuidGenerator.GenerateNewGuid();
        }

        public User(Guid userId)
        {
            Id = userId;
        }
        #region Prop
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Avatar { get; set; }
        public GenderType Gender { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; } = false;
        public DateTime? LastUserUpdate { get; set; }
        /// <summary>
        /// موجودی حساب
        /// </summary>
        public int Inventory { get; set; }
        public SocialNetwork SocialNetwork { get; set; }
        #endregion Prop
        #region Methods

        public void AddSocialNetwork(SocialNetwork socialNetwork)
        {
            SocialNetwork = SocialNetwork.AddSocialNetwork(socialNetwork);
        }

        public void RemoveSocialNetwork()
        {
            SocialNetwork.RemoveAll();
        }

        public static User Intance()
        {
            return new User();
        }

      
        public void UpdateUser(string userName, string firstName, string lastName, string phoneNumber, string email
            , bool isActive, DateTime birthDay, string avatar, GenderType gender)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Avatar = avatar;
            IsActive = isActive;
            PhoneNumber = phoneNumber;
            Email = email;
            BirthDay = birthDay;
            Gender = gender;
        }
        public void UpdateUser(string userName, string firstName, string lastName, string phoneNumber, string email
            , bool isActive, DateTime birthDay, GenderType gender)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            IsActive = isActive;
            PhoneNumber = phoneNumber;
            Email = email;
            BirthDay = birthDay;
            Gender = gender;
        }
        public void DeleteUser()
        {
            IsActive = false;
            IsDelete = true;
        }
        public void Create(string userName, string firstName, string lastName)
        {
            NullOrEmptyDomainDataException.CheckString(userName, "userName");
            NullOrEmptyDomainDataException.CheckString(firstName, "firstName");
            NullOrEmptyDomainDataException.CheckString(lastName, "lastName");
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }
        public void ActiveUser()
        {
            IsActive = true;
            IsDelete = false;
        }
        public static User RegisterUserWith(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                var CreateUser = new User();
                CreateUser.PhoneNumber = phoneNumber;
                CreateUser.UserName = phoneNumber;
                CreateUser.FirstName = "";
                CreateUser.LastName = "";
                return
                    CreateUser;
            }
            throw new InvalidDomainDataException("null or empty phoneNumber");

        }
        #endregion
        #region Relations
        public ICollection<UserClaim> Claims { get; set; }
        public ICollection<UserRole> Roles { get; set; }
        #endregion Relations

       
    }
}
