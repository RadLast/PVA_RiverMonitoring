using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Stations
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Station Station { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Check if user has required role
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
