namespace Deteleschuer.nl.ViewModels;

public class AanvraagRegelViewModel
{
    public int Id { get; set; }
    public string KlantNaam { get; set; } = string.Empty;
    public string KlantEmail { get; set; } = string.Empty;
    public string AbonnementNaam { get; set; } = string.Empty;
    public DateTime AanvraagDatum { get; set; }
    public string StatusTekst { get; set; } = string.Empty;
}
