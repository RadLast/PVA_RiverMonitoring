using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Values
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Value Value { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Value.Timestamp = DateTime.Now;

            _context.Values.Add(Value);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
