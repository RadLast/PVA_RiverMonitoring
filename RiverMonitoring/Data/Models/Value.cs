using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RiverMonitoring.Data.Models
{
    public class Value
    {
        public int Id { get; set; }

        [Required]
        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public Station Station { get; set; }

        [Required]
        public double MeasuredValue { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
