using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data.Models;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Account
{
    /// <summary>
    /// Page model for user logout.
    /// </summary>
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LogoutModel(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// Handles the user logout process.
        /// </summary>
        /// <returns>Redirects to the index page after logout.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
