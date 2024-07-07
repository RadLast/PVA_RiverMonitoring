using System.ComponentModel.DataAnnotations;

public class Station
{
    public int Id { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Title cannot be longer than 30 characters.")]
    public string Title { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Location cannot be longer than 30 characters.")]
    public string Location { get; set; }

    [Required]
    [Range(0, 9999, ErrorMessage = "Timeout must be a four-digit number.")]
    public int Timeout { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string AlertEmail { get; set; }

    [Required]
    [Range(0, 999.999, ErrorMessage = "Flood Warning Value must be a valid number with up to 3 decimal places.")]
    public double FloodWarningValue { get; set; }

    [Required]
    [Range(0, 999.999, ErrorMessage = "Drought Warning Value must be a valid number with up to 3 decimal places.")]
    public double DroughtWarningValue { get; set; }

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public string CreatedByUser { get; set; }
}
