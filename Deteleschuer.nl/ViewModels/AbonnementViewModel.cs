namespace Deteleschuer.nl.ViewModels;

public class AbonnementViewModel
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string ProviderTekst { get; set; } = string.Empty;
    public string ProviderCssKlasse { get; set; } = string.Empty;
    public string ProviderLogoSrc { get; set; } = string.Empty;
    public decimal PrijsPerMaand { get; set; }
    public string Beschrijving { get; set; } = string.Empty;
}
