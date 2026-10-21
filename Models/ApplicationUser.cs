using Microsoft.AspNetCore.Identity;

namespace PAW_PROJECT.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? PozaProfilBase64 { get; set; }
    }
}