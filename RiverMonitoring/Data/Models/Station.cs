using System.ComponentModel.DataAnnotations;

public class Station
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public int Timeout { get; set; }

    [Required]
    public string AlertEmail { get; set; }

    [Required]
    public double FloodWarningValue { get; set; }

    [Required]
    public double DroughtWarningValue { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public string CreatedByUser { get; set; }
}
