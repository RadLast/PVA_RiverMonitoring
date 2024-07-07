using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RiverMonitoring.Data;
using RiverMonitoring.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiverMonitoring.Pages.History
{
    /// <summary>
    /// Page model for viewing the history of measurements.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int? SelectedStationId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool OnlyCritical { get; set; }

        public HistoryViewModel HistoryData { get; set; }

        /// <summary>
        /// Handles the GET request for the history page.
        /// </summary>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task OnGetAsync()
        {
            var stations = await _context.Stations
                .Select(s => new StationViewModel
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

            if (StartDate.HasValue)
            {
                measurementsQuery = measurementsQuery.Where(m => m.Timestamp >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                measurementsQuery = measurementsQuery.Where(m => m.Timestamp <= EndDate.Value);
            }

            if (OnlyCritical)
            {
                measurementsQuery = measurementsQuery.Where(m => m.MeasuredValue > m.Station.FloodWarningValue);
            }

            var measurements = await measurementsQuery.ToListAsync();

            HistoryData = new HistoryViewModel
            {
                Stations = stations,
                Measurements = measurements.Select(m => new MeasurementViewModel
                {
                    Timestamp = m.Timestamp,
                    MeasuredValue = m.MeasuredValue,
                    IsWarning = m.MeasuredValue > m.Station.FloodWarningValue,
                    StationTitle = m.Station.Title
                }).ToList()
            };
        }
    }
}
