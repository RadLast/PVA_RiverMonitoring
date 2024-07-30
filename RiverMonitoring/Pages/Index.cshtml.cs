using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.Data.Models;

namespace RiverMonitoring.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }
        public bool IsAdminAccountExists { get; set; }

        public async Task OnGetAsync()
        {
            IsAdminAccountExists = _userManager.Users.Any(u => u.AccessLevel == "Admin");
        }

        public async Task<IActionResult> OnPostCreateAdminAsync()
        {
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@rivermonitoring.com",
                FullName = "Administrator",
                AccessLevel = "Admin",
                RegistrationDate = DateTime.Now
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin123!");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                StatusMessage = "Admin account created successfully. Username: 'admin', Password: 'Admin123!'";
            }
            else
            {
                StatusMessage = "Error creating admin account: " + string.Join(", ", result.Errors.Select(e => e.Description));
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateStationsAsync()
        {
            var stations = new List<Station>
            {
                new Station { Title = "Station 1", Location = "Mississippi River, Louisiana", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 75, DroughtWarningValue = 25, Latitude = 31.311293, Longitude = -91.396017, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 2", Location = "Missouri River, Missouri", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 80, DroughtWarningValue = 30, Latitude = 38.617241, Longitude = -90.183641, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 3", Location = "Ohio River, Kentucky", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 70, DroughtWarningValue = 20, Latitude = 38.661583, Longitude = -85.164733, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 4", Location = "Colorado River, Arizona", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 85, DroughtWarningValue = 35, Latitude = 36.112026, Longitude = -113.980224, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 5", Location = "Columbia River, Oregon", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 90, DroughtWarningValue = 40, Latitude = 45.663813, Longitude = -122.714571, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 6", Location = "Hudson River, New York", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 65, DroughtWarningValue = 20, Latitude = 40.745418, Longitude = -74.028233, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 7", Location = "Potomac River, Washington D.C.", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 78, DroughtWarningValue = 28, Latitude = 38.896379, Longitude = -77.046497, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 8", Location = "Snake River, Idaho", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 82, DroughtWarningValue = 32, Latitude = 43.515982, Longitude = -112.040329, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 9", Location = "Tennessee River, Tennessee", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 77, DroughtWarningValue = 27, Latitude = 35.601560, Longitude = -87.033360, CreatedOn = DateTime.Now, CreatedByUser = "Admin" },
                new Station { Title = "Station 10", Location = "Arkansas River, Arkansas", Timeout = 60, AlertEmail = "admin@rivermonitoring.com", FloodWarningValue = 88, DroughtWarningValue = 38, Latitude = 34.746841, Longitude = -92.289459, CreatedOn = DateTime.Now, CreatedByUser = "Admin" }
            };

            _context.Stations.AddRange(stations);
            await _context.SaveChangesAsync();

            StatusMessage = "Stations created successfully.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateHistoryDataAsync()
        {
            var values = new List<Value>
            {
                new Value { StationId = 1, MeasuredValue = 10, Timestamp = DateTime.Now.AddMonths(-1) },
                new Value { StationId = 2, MeasuredValue = 70, Timestamp = DateTime.Now.AddMonths(-2) },
                new Value { StationId = 3, MeasuredValue = 10, Timestamp = DateTime.Now.AddMonths(-1) },
                new Value { StationId = 5, MeasuredValue = 5, Timestamp = DateTime.Now.AddMonths(-2) },
                new Value { StationId = 5, MeasuredValue = 80, Timestamp = DateTime.Now.AddMonths(-1) },
                new Value { StationId = 6, MeasuredValue = 5, Timestamp = DateTime.Now.AddMonths(-2) },
                new Value { StationId = 7, MeasuredValue = 5, Timestamp = DateTime.Now.AddMonths(-1) },
                new Value { StationId = 8, MeasuredValue = 5, Timestamp = DateTime.Now.AddMonths(-2) },
                new Value { StationId = 9, MeasuredValue = 10, Timestamp = DateTime.Now.AddMonths(-1) },
                new Value { StationId = 10, MeasuredValue = 88, Timestamp = DateTime.Now.AddMonths(-2) }
            };

            _context.Values.AddRange(values);
            await _context.SaveChangesAsync();

            StatusMessage = "History data created successfully.";
            return RedirectToPage();
        }
    }
}
