using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data.Models;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Users
{
    [Authorize(Roles = "Admin")]
    /// <summary>
    /// Page model for deleting a user.
    /// </summary>
    public class DeleteModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteModel"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public DeleteModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public ApplicationUser UserToDelete { get; set; }

        /// <summary>
        /// Handles the GET request for the delete user page.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>The page result.</returns>
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserToDelete = await _userManager.FindByIdAsync(id);

            if (UserToDelete == null)
            {
                return NotFound();
            }

            return Page();
        }

        /// <summary>
        /// Handles the POST request for deleting a user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Redirects to the index page on success.</returns>
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
