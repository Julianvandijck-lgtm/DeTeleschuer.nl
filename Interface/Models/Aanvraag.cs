namespace  Interface.Models;

public class Aanvraag
{
    public int Id { get; set; }
    public int KlantId { get; set; }
    public int AbonnementId { get; set; }
    public DateTime AanvraagDatum { get; set; }
    public required string Status { get; set; }
    public bool? NummerBehouden { get; set; }
    public required string DigitaleHandtekening  { get; set; } // geen map mar gewoon string reeks tekens
    public DateTime HandtekeningDatum { get; set; }

    public Aanvraag()
    {
        
    }
}
