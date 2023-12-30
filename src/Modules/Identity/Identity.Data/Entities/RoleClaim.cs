using Microsoft.AspNetCore.Identity;

namespace Identity.Data.Entities
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        #region Relations

        public virtual Role Role { get; set; }

        #endregion Relations
    }
}
