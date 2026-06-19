namespace Deteleschuer.nl.ViewModels;

public class AanvraagDetailViewModel
{
    public int Id { get; set; }
    public string KlantNaam { get; set; } = string.Empty;
    public string KlantStraatnaam { get; set; } = string.Empty;
    public string KlantHuisnummer { get; set; } = string.Empty;
    public DateTime KlantGeboortedatum { get; set; }
    public string KlantEmail { get; set; } = string.Empty;
    public string KlantTelefoon { get; set; } = string.Empty;
    public string FotoLegitimatie { get; set; } = string.Empty;
    public string FotoBankpas { get; set; } = string.Empty;
    public string AbonnementNaam { get; set; } = string.Empty;
    public DateTime AanvraagDatum { get; set; }
    public string StatusTekst { get; set; } = string.Empty;
    public bool? NummerBehouden { get; set; }
    public string DigitaleHandtekening { get; set; } = string.Empty;
    public List<NotitieViewModel> Notities { get; set; } = [];
}
