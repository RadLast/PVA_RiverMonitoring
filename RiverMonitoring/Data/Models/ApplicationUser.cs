using Microsoft.AspNetCore.Identity;

namespace RiverMonitoring.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string AccessLevel { get; set; } = "Read";
    }
}
