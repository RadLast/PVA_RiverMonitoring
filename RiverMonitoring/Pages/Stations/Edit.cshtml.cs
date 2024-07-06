using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;

namespace RiverMonitoring.Pages.Stations
{
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

        public async Task<IActionResult> OnPostAsync()
        {
            var stationInDb = await _context.Stations.FindAsync(Station.Id);

            if (stationInDb == null)
            {
                StatusMessage = "Station not found.";
                return RedirectToPage("./Index", new { StatusMessage });
            }

            // Odstraňte CreatedByUser z ModelState, abyste zabránili chybám při validaci
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
            // Neaktualizujte CreatedByUser a CreatedOn, nechte je tak, jak jsou v databázi

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

        private bool StationExists(int id)
        {
            return _context.Stations.Any(e => e.Id == id);
        }
    }
}
