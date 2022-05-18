using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SchoolApp.ID.Server.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolApp.ID.Server.Services
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AppClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            if (principal.Identity is ClaimsIdentity claims)
            {
                claims.AddClaims(
                    new[] {
                        new Claim("FirstName", user.FirstName ?? string.Empty),
                        new Claim("LastName", user.LastName ?? string.Empty)
                    });

                claims.AddClaims(roles.Select(s => new Claim(ClaimTypes.Role, s ?? string.Empty)));

                principal = new ClaimsPrincipal(claims);
            }

            return principal;
        }
    }
}
