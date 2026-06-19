using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeAanvraagRepository : IAanvraagRepository
{
    public AanvraagDto? OpgeslagenAanvraag { get; private set; }

    public void Opslaan(AanvraagDto aanvraag) => OpgeslagenAanvraag = aanvraag;

    public List<AanvraagOverzichtDto> HaalAlleOp() =>
    [
        new() { Id = 1, KlantNaam = "Jan Jansen",   KlantEmail = "jan@example.com",  AbonnementNaam = "Basis Odido",    Provider = "Odido",    AanvraagDatum = DateTime.Now, Status = "Nieuw" },
        new() { Id = 2, KlantNaam = "Sara Bakker",  KlantEmail = "sara@example.com", AbonnementNaam = "Plus Vodafone",  Provider = "Vodafone", AanvraagDatum = DateTime.Now, Status = "In behandeling" },
        new() { Id = 3, KlantNaam = "Tom de Vries", KlantEmail = "tom@example.com",  AbonnementNaam = "Premium Odido",  Provider = "Odido",    AanvraagDatum = DateTime.Now, Status = "Afgerond" },
        new() { Id = 4, KlantNaam = "Lisa Smit",    KlantEmail = "lisa@example.com", AbonnementNaam = "Basis Vodafone", Provider = "Vodafone", AanvraagDatum = DateTime.Now, Status = "Nieuw" }
    ];

    public AanvraagDetailDto? HaalDetailOp(int id) => null;

    public void StatusBijwerken(int id, string status) { }
}
