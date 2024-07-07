using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data.Models;
using RiverMonitoring.ViewModels;
using System.Text;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Account
{
    /// <summary>
    /// Page model for user registration.
    /// </summary>
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputRegisterViewModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public void OnGet(string returnUrl = null)
        {
            StatusMessage = "";
            ReturnUrl = returnUrl;
        }

        /// <summary>
        /// Handles the user registration process.
        /// </summary>
        /// <param name="returnUrl">URL to redirect to after registration.</param>
        /// <returns>Redirects to the return URL on successful registration.</returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    FullName = Input.FullName,
                    AccessLevel = "Read", // Default AccessLevel
                    RegistrationDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    StatusMessage = "User registered successfully.";
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                var errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.AppendLine(error.Description);
                }

                StatusMessage = $"Error: Unable to register user.\n{errors}";
            }

            return Page();
        }
    }
}
