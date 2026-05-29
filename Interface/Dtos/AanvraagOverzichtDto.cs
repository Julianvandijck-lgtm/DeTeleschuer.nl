namespace Interface.Dtos;

public class AanvraagOverzichtDto
{
    public int Id { get; set; }
    public required string KlantNaam { get; set; }
    public required string KlantEmail { get; set; }
    public required string AbonnementNaam { get; set; }
    public DateTime AanvraagDatum { get; set; }
    public required string Status { get; set; }
}