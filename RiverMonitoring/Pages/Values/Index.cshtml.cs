using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.Values
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Value> Values { get; set; }

        public async Task OnGetAsync()
        {
            Values = await _context.Values.Include(v => v.Station).ToListAsync();
        }
    }
}
