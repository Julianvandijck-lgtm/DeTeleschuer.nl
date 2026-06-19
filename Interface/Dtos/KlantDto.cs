namespace Interface.Dtos;

public class KlantDto
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string Straatnaam { get; set; } = string.Empty;
    public string Huisnummer { get; set; } = string.Empty;
    public DateOnly GeboorteDatum { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Telefoon { get; set; } = string.Empty;
    public string FotoID { get; set; } = string.Empty;
    public string FotoBankpas { get; set; } = string.Empty;
}
