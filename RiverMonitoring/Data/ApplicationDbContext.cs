using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data.Models;
using RiverMonitoring.Data.ViewModels;

namespace RiverMonitoring.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<RiverMonitoring.Data.ViewModels.EditUserViewModel> EditUserViewModel { get; set; } = default!;
    }
}
