using System.ComponentModel.DataAnnotations;

namespace Deteleschuer.nl.ViewModels;

public class LoginViewModel
{
    [Required] public string Gebruikersnaam { get; set; } = "";
    [Required] public string Wachtwoord { get; set; } = "";
}