using Interface.Models;

namespace Interface.Dtos;

public class AanvraagDetailDto
{
    public int Id { get; set; }
    public required string KlantNaam { get; set; }
    public required string KlantAdres { get; set; }
    public DateTime KlantGeboortedatum { get; set; }
    public required string KlantEmail { get; set; }
    public required string KlantTelefoon { get; set; }
    public required string FotoLegitimatie { get; set; }
    public required string FotoBankpas { get; set; }
    public required string AbonnementNaam { get; set; }
    public DateTime AanvraagDatum { get; set; }
    public required string Status { get; set; }
    public bool? NummerBehouden { get; set; }
    public required string DigitaleHandtekening { get; set; }
    public List<Notitie> Notities { get; set; } = new(); // list omdat het een aparte tabel krijgt in de database, maar ook omdat meerdere noties gescrhevn kunnen worden bij een aanvraag detail 
}                                               // met = new maak ik een lege lijst aan bij de aanvraag voor notieties zodat ie altijd open staat en bijgevuld kan worden