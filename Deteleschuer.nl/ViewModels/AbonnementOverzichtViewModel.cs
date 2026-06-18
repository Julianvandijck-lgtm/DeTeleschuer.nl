namespace Deteleschuer.nl.ViewModels;

public class AbonnementOverzichtViewModel
{
    public List<AbonnementViewModel> Abonnementen { get; set; } = [];
    public string? GekozenProvider { get; set; }
}
