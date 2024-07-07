using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Stations
{
    [Authorize(Roles = "Admin,Write")]
    /// <summary>
    /// Page model for viewing the details of a station.
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Station Station { get; set; }

        /// <summary>
        /// Handles the GET request for the details page of a station.
        /// </summary>
        /// <param name="id">The ID of the station.</param>
        /// <returns>The page result.</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Check if user has the required role
            if (!User.IsInRole("Write") && !User.IsInRole("Admin"))
            {
                return RedirectToPage("/AccessDenied");
            }

            Station = await _context.Stations.FindAsync(id);

            if (Station == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
