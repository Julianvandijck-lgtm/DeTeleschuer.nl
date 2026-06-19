using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeAanvraagRepository : IAanvraagRepository
{
    public AanvraagDto? OpgeslagenAanvraag { get; private set; }

    public void Opslaan(AanvraagDto aanvraag) => OpgeslagenAanvraag = aanvraag;

    public List<AanvraagOverzichtDto> HaalAlleOp() => [];

    public AanvraagDetailDto? HaalDetailOp(int id) => null;

    public void StatusBijwerken(int id, string status) { }
}
