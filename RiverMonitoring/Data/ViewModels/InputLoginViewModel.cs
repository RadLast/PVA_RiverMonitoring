using System.ComponentModel.DataAnnotations;

namespace RiverMonitoring.ViewModels
{
    public class InputLoginViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "Username cannot be longer than 30 characters.")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
