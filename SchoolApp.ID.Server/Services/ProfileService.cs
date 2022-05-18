using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using SchoolApp.ID.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolApp.ID.Server.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
        }

        //This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)**
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context?.Subject ?? throw new ArgumentNullException(nameof(context));

            var subjectId = subject.Claims?.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            var user = await _userManager.FindByIdAsync(subjectId);

            if (user == null)
                throw new ArgumentException("Invalid subject identifier");

            var claims = await GetClaimsFromUser(user, subject);

            context.IssuedClaims = claims.ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subject = context?.Subject ?? throw new ArgumentNullException(nameof(context));

            var subjectId = subject.Claims?.Where(x => x.Type == "sub")?.FirstOrDefault()?.Value;

            var user = await _userManager.FindByIdAsync(subjectId);

            context.IsActive = false;

            if (user != null)
            {
                if (_userManager.SupportsUserSecurityStamp)
                {
                    var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
                    if (security_stamp != null)
                    {
                        var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
                        if (db_security_stamp != security_stamp)
                            return;
                    }
                }

                context.IsActive =
                    !user.LockoutEnabled ||
                    !user.LockoutEnd.HasValue ||
                    user.LockoutEnd <= DateTime.Now;
            }
        }

        private async Task<IEnumerable<Claim>> GetClaimsFromUser(ApplicationUser user, ClaimsPrincipal? subject)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim("FirstName", user.FirstName ?? string.Empty),
                new Claim("LastName", user.LastName ?? string.Empty)
            };

            var roles = await _userManager.GetRolesAsync(user);

            claims.AddRange(roles.Select(s => new Claim(ClaimTypes.Role, s ?? string.Empty)));

            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email)
                });
            }

            return await Task.FromResult(claims);
        }
    }
}
