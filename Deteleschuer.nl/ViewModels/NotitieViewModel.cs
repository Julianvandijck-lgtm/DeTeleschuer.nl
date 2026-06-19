namespace Deteleschuer.nl.ViewModels;

public class NotitieViewModel
{
    public int Id { get; set; }
    public int AanvraagId { get; set; }
    public string Tekst { get; set; } = string.Empty;
    public DateTime DatumAangemaakt { get; set; }
}
