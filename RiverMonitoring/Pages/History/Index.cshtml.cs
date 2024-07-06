using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.History
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? SelectedStationId { get; set; }

        public HistoryViewModel HistoryData { get; set; }

        public async Task OnGetAsync()
        {
            var stations = await _context.Stations
                .Select(s => new HistoryViewModel.StationDto
                {
                    Id = s.Id,
                    Title = s.Title
                }).ToListAsync();

            var measurementsQuery = _context.Values
                .Include(v => v.Station)
                .AsQueryable();

            if (SelectedStationId.HasValue)
            {
                measurementsQuery = measurementsQuery.Where(m => m.StationId == SelectedStationId.Value);
            }

            var measurements = await measurementsQuery.ToListAsync();

            HistoryData = new HistoryViewModel
            {
                Stations = stations,
                Measurements = measurements.Select(m => new HistoryViewModel.Measurement
                {
                    Timestamp = m.Timestamp,
                    MeasuredValue = m.MeasuredValue,
                    IsWarning = m.MeasuredValue > m.Station.FloodWarningValue, // Zde m?žete upravit logiku varování
                    StationTitle = m.Station.Title
                }).ToList()
            };
        }
    }
}
