using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RiverMonitoring.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(30, ErrorMessage = "Username cannot be longer than 30 characters.")]
        [Display(Name = "User Name")]
        public string FullName { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string AccessLevel { get; set; } = "Read";
    }
}
