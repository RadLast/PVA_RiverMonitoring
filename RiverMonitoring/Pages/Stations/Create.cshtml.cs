using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Stations
{
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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
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
