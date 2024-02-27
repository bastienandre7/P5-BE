using Microsoft.AspNetCore.Identity;

namespace ExpressVoitureApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
