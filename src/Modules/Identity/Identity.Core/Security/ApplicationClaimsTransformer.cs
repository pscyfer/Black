using Identity.Core.Dto.User;
using Identity.Core.Exceptions;
using Identity.Core.Services;
using System.Security.Claims;
using System.Security.Principal;
using Common.Application;
using Identity.Core.Repository.Users;
using Microsoft.AspNetCore.Authentication;

namespace Identity.Core.Security
{
    public class CustomClaimsPrincipal : ClaimsPrincipal
    {

        public CustomClaimsPrincipal(
            IPrincipal principal) : base(principal)
        {

        }
    }




    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IUserRepository _userRepository;
        public ClaimsTransformer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            var customPrincipal = new CustomClaimsPrincipal(principal) as ClaimsPrincipal;
            if (customPrincipal.Identity is { IsAuthenticated: true })
            {
                var user = await _userRepository.FindByNameAsync(customPrincipal.Identity.Name);
                if (user == null) return customPrincipal;
                var getRoles = await _userRepository.GetRolesAsync(user);

                if (!customPrincipal.HasClaim(x => x.Type == "FullName"))
                    claimsIdentity.AddClaim(new Claim("FullName", user.FirstName + " " + user.LastName));

                if (!customPrincipal.HasClaim(x => x.Type == "RoleName"))
                    claimsIdentity.AddClaim(new Claim("RoleName", string.Join(",", getRoles)));
                if (!customPrincipal.HasClaim(x => x.Type == "Avatar"))
                    claimsIdentity.AddClaim(new Claim("Avatar", user.Avatar.GenerateStaticUrl()));

                if (!customPrincipal.HasClaim(x => x.Type == "UserId"))
                    claimsIdentity.AddClaim(new Claim("UserId", user.Id.ToString()));
                customPrincipal.AddIdentity(claimsIdentity);
            }
            return customPrincipal;
        }
    }
}
