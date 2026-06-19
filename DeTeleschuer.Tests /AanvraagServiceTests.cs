using DeTeleschuer.Tests.Fakes;
using Interface.Models;
using Logic.Services;

namespace DeTeleschuer.Tests;

public class AanvraagServiceTests
{
    private static Klant MaakKlant() => new(
        naam: "Jan Janssen",
        straatnaam: "Hoofdstraat",
        huisnummer: "10",
        geboorteDatum: new DateOnly(1990, 1, 1),
        email: "jan@example.com",
        telefoon: "0612345678",
        fotoId: "foto.jpg",
        fotoBankpas: "bankpas.jpg"
    );

    [Fact]
    public void NieuweKlant_WordtOpgeslagenEnAanvraagKrijgtDiensKlantId()
    {
        var klantRepo = new FakeKlantRepository(bestaandId: null);
        var aanvraagRepo = new FakeAanvraagRepository();
        var service = new AanvraagService(klantRepo, aanvraagRepo);

        service.AanvraagOpslaan(MaakKlant(), 1, false, "data:image/png;base64,abc");

        Assert.True(klantRepo.OpslaanAangeroepen);
        Assert.Equal(klantRepo.TeruggegvenId, aanvraagRepo.OpgeslagenAanvraag!.KlantId);
    }

    [Fact]
    public void BestaandeKlantOpEmail_WordtHergebruikt_GeenNieuweInsert()
    {
        var klantRepo = new FakeKlantRepository(bestaandId: 99);
        var aanvraagRepo = new FakeAanvraagRepository();
        var service = new AanvraagService(klantRepo, aanvraagRepo);

        service.AanvraagOpslaan(MaakKlant(), 1, false, "data:image/png;base64,abc");

        Assert.False(klantRepo.OpslaanAangeroepen);
        Assert.Equal(99, aanvraagRepo.OpgeslagenAanvraag!.KlantId);
    }

    [Fact]
    public void NieuweAanvraag_KrijgtStatusNieuw()
    {
        var aanvraagRepo = new FakeAanvraagRepository();
        var service = new AanvraagService(new FakeKlantRepository(), aanvraagRepo);

        service.AanvraagOpslaan(MaakKlant(), 1, null, "data:image/png;base64,abc");

        Assert.Equal("Nieuw", aanvraagRepo.OpgeslagenAanvraag!.Status);
    }
}
