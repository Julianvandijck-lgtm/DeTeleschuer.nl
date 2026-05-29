using System.ComponentModel.DataAnnotations;

namespace Deteleschuer.nl.ViewModels;

public class PersoonsgegevensViewModel
{
    [Required]
    public string Naam { get; set; }
    [Required]
    public string Adres { get; set; }
    [Required]
    public DateTime Geboortedatum { get; set; }
    [Required] [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Telefoon { get; set; }
    [Required]
    public bool NummerBehouden { get; set; }
}
    