namespace RiverMonitoring.ViewModels
{
    public class MeasurementViewModel
    {
        public string StationTitle { get; set; }
        public DateTime Timestamp { get; set; }
        public double MeasuredValue { get; set; }
        public bool IsWarning { get; set; }
    }
}
