using System.Collections.Generic;

namespace RiverMonitoring.ViewModels
{
    public class HistoryViewModel
    {
        public List<StationViewModel> Stations { get; set; }
        public List<MeasurementViewModel> Measurements { get; set; }
    }
}
