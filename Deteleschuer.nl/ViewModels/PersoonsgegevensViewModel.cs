using System.ComponentModel.DataAnnotations;

namespace Deteleschuer.nl.ViewModels;

public class PersoonsgegevensViewModel
{
    [Required]
    public string Naam { get; set; } = string.Empty;
    [Required]
    public string Straatnaam { get; set; } = string.Empty;
    [Required]
    public string Huisnummer { get; set; } = string.Empty;
    [Required]
    public DateTime? Geboortedatum { get; set; }
    [Required] [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Telefoon { get; set; } = string.Empty;
    [Required]
    public bool NummerBehouden { get; set; }
}
