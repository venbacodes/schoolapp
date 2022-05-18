using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            var query = Request.HttpContext.Request.Query;

            string redirectTo = "index";

            if (query.ContainsKey("returnTo") && !string.IsNullOrWhiteSpace(query["returnTo"]))
            {
                redirectTo = query["returnTo"];
            }

            return Challenge(new AuthenticationProperties() { RedirectUri = redirectTo }, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}
