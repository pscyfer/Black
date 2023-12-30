using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Entities
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public virtual User User { get; set; }
    }
}
