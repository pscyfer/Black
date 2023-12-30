using Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data.Configurations
{
    internal static class IdentityConfig
    {
        public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
        {
            #region TableOfNames
            modelBuilder.Entity<User>().ToTable("AppUsers","Identity");
            modelBuilder.Entity<Role>().ToTable("AppRoles", "Identity");
            modelBuilder.Entity<UserRole>().ToTable("AppUserRoles", "Identity");
            modelBuilder.Entity<RoleClaim>().ToTable("AppRoleClaims", "Identity");
            modelBuilder.Entity<UserClaim>().ToTable("AppUserClaims", "Identity");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins", "Identity");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens", "Identity");
            #endregion

            #region UserConfig
            modelBuilder.Entity<User>().OwnsOne(x => x.SocialNetwork, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.Property(x => x.Telegram).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.WhatsApp).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.Instagram).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.Discord).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.Linkdin).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.GitHub).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.YouTube).HasDefaultValue("").HasMaxLength(50);
                ownedNavigationBuilder.Property(x => x.Email).HasDefaultValue("").HasMaxLength(50);
            });
            #endregion

            #region RoleConfig

            var roleBuilder = modelBuilder.Entity<Role>();

            //roleBuilder.Property(x => x.Permissions).HasColumnType("xml")
            //    .IsRequired(false);

            //roleBuilder.Ignore(x => x.XmlPermission);
            #endregion

            #region UserRoleConfig
            modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users).HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.User)
                .WithMany(role => role.Roles).HasForeignKey(r => r.UserId);
            #endregion

            #region RoleClaimConfig
            modelBuilder.Entity<RoleClaim>()
                .HasOne(roleClaim => roleClaim.Role)
                .WithMany(claim => claim.Claims).HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<UserClaim>()
                .HasOne(userClaim => userClaim.User)
                .WithMany(claim => claim.Claims).HasForeignKey(c => c.UserId);
            #endregion

            #region UserLoginConfig
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasKey(x => x.UserId);
            #endregion

        }
    }
}
