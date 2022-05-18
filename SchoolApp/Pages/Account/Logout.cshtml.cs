using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolApp.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            foreach (var cookie in Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

            var query = Request.HttpContext.Request.Query;

            string redirectTo = "index";

            if (query.ContainsKey("returnTo") && !string.IsNullOrWhiteSpace(query["returnTo"]))
            {
                redirectTo = query["returnTo"];
            }

            return SignOut(new AuthenticationProperties() { RedirectUri = redirectTo },
             OpenIdConnectDefaults.AuthenticationScheme,
             CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
