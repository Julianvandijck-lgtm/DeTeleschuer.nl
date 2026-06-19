namespace Interface.Dtos;

public class AanvraagDto
{
    public int Id { get; set; }
    public int KlantId { get; set; }
    public int AbonnementId { get; set; }
    public DateTime AanvraagDatum { get; set; }
    public string Status { get; set; } = string.Empty;
    public bool? NummerBehouden { get; set; }
    public string DigitaleHandtekening { get; set; } = string.Empty;
    public DateTime HandtekeningDatum { get; set; }
}
