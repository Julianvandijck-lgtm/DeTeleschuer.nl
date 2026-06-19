using DeTeleschuer.Tests.Fakes;
using Interface.Enums;
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

    private static AanvraagService MaakService(FakeAanvraagRepository? aanvraagRepo = null) =>
        new(new FakeKlantRepository(), aanvraagRepo ?? new FakeAanvraagRepository());

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

    [Fact]
    public void HaalOverzicht_OpStatus_GeeftAlleenJuisteStatusTerug()
    {
        var resultaat = MaakService().HaalOverzicht(status: AanvraagStatus.Nieuw);

        Assert.All(resultaat, r => Assert.Equal("Nieuw", r.Status));
        Assert.Equal(2, resultaat.Count);
    }

    [Fact]
    public void HaalOverzicht_OpProvider_GeeftAlleenJuisteProviderTerug()
    {
        var resultaat = MaakService().HaalOverzicht(provider: Provider.Vodafone);

        Assert.All(resultaat, r => Assert.Equal("Vodafone", r.Provider));
        Assert.Equal(2, resultaat.Count);
    }

    [Fact]
    public void HaalOverzicht_OpZoektermNaam_GeeftMatchendeResultatenTerug()
    {
        var resultaat = MaakService().HaalOverzicht(zoekterm: "jansen");

        Assert.Single(resultaat);
        Assert.Equal("Jan Jansen", resultaat[0].KlantNaam);
    }

    [Fact]
    public void HaalOverzicht_OpZoektermEmail_GeeftMatchendeResultatenTerug()
    {
        var resultaat = MaakService().HaalOverzicht(zoekterm: "sara@");

        Assert.Single(resultaat);
        Assert.Equal("Sara Bakker", resultaat[0].KlantNaam);
    }

    [Fact]
    public void HaalOverzicht_GecombineerdFilter_GeeftGefilterdeResultaten()
    {
        var resultaat = MaakService().HaalOverzicht(status: AanvraagStatus.Nieuw, provider: Provider.Vodafone);

        Assert.Single(resultaat);
        Assert.Equal("Lisa Smit", resultaat[0].KlantNaam);
    }

    [Fact]
    public void HaalOverzicht_GeenMatches_GeeftLegeLijst()
    {
        var resultaat = MaakService().HaalOverzicht(zoekterm: "onbekend");

        Assert.Empty(resultaat);
    }
}
