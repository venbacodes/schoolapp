using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolApp.Pages.Account
{
    public class ProfileModel : PageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Redirect("/account/login?returnTo=/account/profile");
            }

            var claims = User.Claims;

            FirstName = claims.FirstOrDefault(f => f.Type.Equals("FirstName", System.StringComparison.OrdinalIgnoreCase))?.Value;
            LastName = claims.FirstOrDefault(f => f.Type.Equals("LastName", System.StringComparison.OrdinalIgnoreCase))?.Value;
            Email = claims.FirstOrDefault(f => f.Type.Equals(JwtClaimTypes.Email, System.StringComparison.OrdinalIgnoreCase))?.Value;

            return Page();
        }
    }
}
