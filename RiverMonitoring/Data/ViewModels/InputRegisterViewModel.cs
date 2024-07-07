using System.ComponentModel.DataAnnotations;

namespace RiverMonitoring.ViewModels
{
    public class InputRegisterViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Username cannot be longer than 30 characters.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(50, ErrorMessage = "Full Name cannot be longer than 50 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}
