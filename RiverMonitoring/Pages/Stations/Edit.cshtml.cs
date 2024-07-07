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
    /// Page model for editing a station.
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Station Station { get; set; }
        public string StatusMessage { get; set; }

        /// <summary>
        /// Handles the GET request for the edit station page.
        /// </summary>
        /// <param name="id">The ID of the station to edit.</param>
        /// <returns>The page result.</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Station = await _context.Stations.FindAsync(id);

            if (Station == null)
            {
                StatusMessage = "Station not found.";
                return RedirectToPage("./Index", new { StatusMessage });
            }
            return Page();
        }

        /// <summary>
        /// Handles the POST request for updating a station.
        /// </summary>
        /// <returns>Redirects to the index page on success or returns the page with an error message on failure.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var stationInDb = await _context.Stations.FindAsync(Station.Id);

            if (stationInDb == null)
            {
                StatusMessage = "Station not found.";
                return RedirectToPage("./Index", new { StatusMessage });
            }

            // Remove CreatedByUser from ModelState to avoid validation errors
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

            stationInDb.Title = Station.Title;
            stationInDb.Location = Station.Location;
            stationInDb.Timeout = Station.Timeout;
            stationInDb.AlertEmail = Station.AlertEmail;
            stationInDb.FloodWarningValue = Station.FloodWarningValue;
            stationInDb.DroughtWarningValue = Station.DroughtWarningValue;
            stationInDb.Latitude = Station.Latitude;
            stationInDb.Longitude = Station.Longitude;
            // Do not update CreatedByUser and CreatedOn, keep them as they are in the database

            try
            {
                await _context.SaveChangesAsync();
                TempData["StatusMessage"] = $"Station '{Station.Title}' was updated successfully.";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationExists(Station.Id))
                {
                    StatusMessage = "Station no longer exists.";
                    return RedirectToPage("./Index", new { StatusMessage });
                }
                else
                {
                    StatusMessage = "An error occurred while updating the station.";
                    throw;
                }
            }
        }

        /// <summary>
        /// Checks if a station with the given ID exists.
        /// </summary>
        /// <param name="id">The ID of the station.</param>
        /// <returns>True if the station exists, false otherwise.</returns>
        private bool StationExists(int id)
        {
            return _context.Stations.Any(e => e.Id == id);
        }
    }
}
