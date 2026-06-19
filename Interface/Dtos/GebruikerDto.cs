namespace Interface.Dtos;

public class GebruikerDto
{
    public int Id { get; set; }
    public string Gebruikersnaam { get; set; } = string.Empty;
    public string WachtwoordHash { get; set; } = string.Empty;
}
