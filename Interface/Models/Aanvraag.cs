using Interface.Enums;

namespace Interface.Models;

public class Aanvraag
{
    public int Id { get; }
    public int KlantId { get; }
    public int AbonnementId { get; }
    public DateTime AanvraagDatum { get; }
    public AanvraagStatus Status { get; }
    public bool? NummerBehouden { get; }
    public string DigitaleHandtekening { get; }
    public DateTime HandtekeningDatum { get; }

    public Aanvraag(int id, int klantId, int abonnementId, DateTime aanvraagDatum, AanvraagStatus status, bool? nummerBehouden, string digitaleHandtekening, DateTime handtekeningDatum)
    {
        if (abonnementId <= 0)
            throw new ArgumentException("AbonnementId moet groter zijn dan 0.", nameof(abonnementId));
        if (klantId < 0)
            throw new ArgumentException("KlantId mag niet negatief zijn.", nameof(klantId));
        if (string.IsNullOrWhiteSpace(digitaleHandtekening))
            throw new ArgumentException("Digitale handtekening mag niet leeg zijn.", nameof(digitaleHandtekening));
        if (!Enum.IsDefined(typeof(AanvraagStatus), status))
            throw new ArgumentException("Ongeldige aanvraagstatus.", nameof(status));

        Id = id;
        KlantId = klantId;
        AbonnementId = abonnementId;
        AanvraagDatum = aanvraagDatum;
        Status = status;
        NummerBehouden = nummerBehouden;
        DigitaleHandtekening = digitaleHandtekening;
        HandtekeningDatum = handtekeningDatum;
    }
}
