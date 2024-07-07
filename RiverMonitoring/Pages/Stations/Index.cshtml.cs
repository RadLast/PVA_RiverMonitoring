using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Stations
{
    /// <summary>
    /// Page model for listing all stations.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Station> Stations { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Handles the GET request for listing all stations.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task OnGetAsync()
        {
            Stations = await _context.Stations.ToListAsync();
        }

        /// <summary>
        /// Handles the POST request for deleting a station.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>Redirects to the index page on success.</returns>
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var station = await _context.Stations.FindAsync(id);

            if (station != null)
            {
                _context.Stations.Remove(station);
                await _context.SaveChangesAsync();
                StatusMessage = "Station deleted successfully.";
            }

            return RedirectToPage();
        }
    }
}
