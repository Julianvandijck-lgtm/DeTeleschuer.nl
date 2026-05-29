namespace Interface.Models;

public class Notitie
{
    public int Id { get; set; }
    public int AanvraagId { get; set; }
    public required string Tekst { get; set; }
    public DateTime DatumAangemaakt { get; set; }
}