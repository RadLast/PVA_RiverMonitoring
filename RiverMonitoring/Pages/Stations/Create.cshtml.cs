using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Stations
{
    [Authorize(Roles = "Admin,Write")]
    /// <summary>
    /// Page model for creating a new station.
    /// </summary>
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }
        public string StatusMessage { get; set; }

        /// <summary>
        /// Handles the GET request for the create station page.
        /// </summary>
        /// <returns>The page result.</returns>
        public IActionResult OnGet()
        {
            return Page();
        }

        /// <summary>
        /// Handles the POST request for creating a new station.
        /// </summary>
        /// <returns>Redirects to the index page on success or returns the page with an error message on failure.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // Remove CreatedByUser from ModelState to avoid validation error
            ModelState.Remove("Station.CreatedByUser");

            if (!ModelState.IsValid)
            {
                StatusMessage = "Invalid data:";
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        StatusMessage += $"\n- {state.Key}: {state.Value.Errors[0].ErrorMessage}";
                    }
                }
                return Page();
            }

            try
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                if (user != null)
                {
                    Station.CreatedByUser = user.FullName;
                }
                else
                {
                    StatusMessage = "Unable to find the current user.";
                    return Page();
                }

                _context.Stations.Add(Station);
                await _context.SaveChangesAsync();

                StatusMessage = "Station created successfully.";
                return RedirectToPage("./Index", new { StatusMessage });
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                return Page();
            }
        }
    }
}
