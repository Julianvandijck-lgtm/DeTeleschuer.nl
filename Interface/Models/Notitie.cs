namespace Interface.Models;

public class Notitie
{
    public int Id { get; }
    public int AanvraagId { get; }
    public string Tekst { get; }
    public DateTime DatumAangemaakt { get; }

    public Notitie(int aanvraagId, string tekst, DateTime datumAangemaakt, int id = 0)
    {
        if (aanvraagId <= 0) throw new ArgumentException("AanvraagId moet groter zijn dan 0.", nameof(aanvraagId));
        if (string.IsNullOrWhiteSpace(tekst)) throw new ArgumentException("Tekst mag niet leeg zijn.", nameof(tekst));

        Id = id;
        AanvraagId = aanvraagId;
        Tekst = tekst;
        DatumAangemaakt = datumAangemaakt;
    }
}
