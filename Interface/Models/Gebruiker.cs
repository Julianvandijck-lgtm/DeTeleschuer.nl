namespace Interface.Models;

public class Gebruiker
{
    public int Id { get; set; }
    public required string Gebruikersnaam { get; set; }
    public required string WachtwoordHash { get; set; }
}