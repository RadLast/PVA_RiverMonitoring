using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;

namespace RiverMonitoring.Pages.Stations
{
    [Authorize(Roles = "Admin,Write")]
    /// <summary>
    /// Page model for deleting a station.
    /// </summary>
    public class DeleteModel : PageModel
    {
        private readonly RiverMonitoring.Data.ApplicationDbContext _context;

        public DeleteModel(RiverMonitoring.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; } = default!;

        /// <summary>
        /// Handles the GET request for the delete station page.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>The page result.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Stations.FirstOrDefaultAsync(m => m.Id == id);

            if (station == null)
            {
                return NotFound();
            }
            else
            {
                Station = station;
            }
            return Page();
        }

        /// <summary>
        /// Handles the POST request for deleting a station.
        /// </summary>
        /// <param name="id">The ID of the station to delete.</param>
        /// <returns>Redirects to the index page on success.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Stations.FindAsync(id);
            if (station != null)
            {
                Station = station;
                _context.Stations.Remove(Station);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
