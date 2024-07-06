using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace RiverMonitoring.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> Users { get; set; }
        public string StatusMessage { get; set; }

        public void OnGet(string statusMessage)
        {
            StatusMessage = statusMessage;
            Users = _context.Users.ToList();
        }
    }
}
