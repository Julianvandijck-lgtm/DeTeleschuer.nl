namespace Deteleschuer.nl.ViewModels;

public class AanvraagOverzichtViewModel
{
    public List<AanvraagRegelViewModel> Aanvragen { get; set; } = [];
    public string? GekozenStatus { get; set; }
    public string? GekozenProvider { get; set; }
    public string? Zoekterm { get; set; }
}
