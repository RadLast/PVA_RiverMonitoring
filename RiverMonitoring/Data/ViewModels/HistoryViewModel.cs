using System;
using System.Collections.Generic;

namespace RiverMonitoring.ViewModels
{
    public class HistoryViewModel
    {
        public List<StationDto> Stations { get; set; }
        public List<Measurement> Measurements { get; set; }
        public int? SelectedStationId { get; set; }

        public class StationDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }

        public class Measurement
        {
            public DateTime Timestamp { get; set; }
            public double MeasuredValue { get; set; }
            public bool IsWarning { get; set; }
            public string StationTitle { get; set; }
        }
    }
}
