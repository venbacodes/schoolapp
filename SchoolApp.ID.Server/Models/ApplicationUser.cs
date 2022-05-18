using Microsoft.AspNetCore.Identity;

namespace SchoolApp.ID.Server.Models
{

    public class ApplicationUser : IdentityUser
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
    }
}
