using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeNotitieRepository : INotitieRepository
{
    public List<NotitieDto> OpgeslagenNotities { get; } = [];
    public int? BijgewerktId { get; private set; }
    public string? BijgewerktTekst { get; private set; }

    public List<NotitieDto> HaalOpVoorAanvraag(int aanvraagId) =>
        OpgeslagenNotities.Where(n => n.AanvraagId == aanvraagId).ToList();

    public void Opslaan(NotitieDto notitie) => OpgeslagenNotities.Add(notitie);

    public void Bijwerken(int id, string tekst)
    {
        BijgewerktId = id;
        BijgewerktTekst = tekst;
    }
}
