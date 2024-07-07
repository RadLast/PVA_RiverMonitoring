using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Users
{
    [Authorize(Roles = "Admin")]
    /// <summary>
    /// Page model for editing a user.
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser EditUser { get; set; }
        public string StatusMessage { get; set; }

        /// <summary>
        /// Handles the GET request for the edit user page.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>The page result.</returns>
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EditUser = await _context.Users.FindAsync(id);

            if (EditUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        /// <summary>
        /// Handles the POST request for updating a user.
        /// </summary>
        /// <returns>Redirects to the index page on success or returns the page with an error message on failure.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                StatusMessage = "Invalid data.";
                return Page();
            }

            var userInDb = await _context.Users.FindAsync(EditUser.Id);

            if (userInDb == null)
            {
                return NotFound();
            }

            userInDb.Email = EditUser.Email;
            userInDb.FullName = EditUser.FullName;
            userInDb.AccessLevel = EditUser.AccessLevel;

            await _context.SaveChangesAsync();

            StatusMessage = $"User '{EditUser.UserName}' was updated successfully.";
            return RedirectToPage("./Index", new { StatusMessage });
        }
    }
}
