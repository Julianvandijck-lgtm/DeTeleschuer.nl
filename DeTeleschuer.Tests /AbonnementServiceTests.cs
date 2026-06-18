using DeTeleschuer.Tests.Fakes;
using Interface.Enums;
using Interface.Models;
using Logic.Services;

namespace DeTeleschuer.Tests;

public class AbonnementServiceTests
{
    [Fact]
    public void HaalOverzichtOp_ZonderFilter_GeeftAlleActieveAbonnementenTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        List<Abonnement> result = service.HaalOverzichtOp();

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void HaalOverzichtOp_MetProviderFilter_GeeftAlleenAbonnementenVanDieProviderTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        List<Abonnement> result = service.HaalOverzichtOp(Provider.Odido);

        Assert.Equal(2, result.Count);
        Assert.All(result, a => Assert.Equal(Provider.Odido, a.Provider));
    }

    [Fact]
    public void HaalOverzichtOp_GeeftModelTerug_MetJuisteVelden()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        Abonnement model = service.HaalOverzichtOp().First();

        Assert.Equal(1, model.Id);
        Assert.Equal("Basis", model.Naam);
        Assert.Equal(Provider.Odido, model.Provider);
        Assert.Equal(10m, model.PrijsPerMaand);
        Assert.Equal("Basis pakket", model.Beschrijving);
    }

    [Fact]
    public void HaalOverzichtOp_LegeFakeRepository_GeeftLegeListTerug()
    {
        var service = new AbonnementService(new FakeLegeAbonnementRepository());

        List<Abonnement> result = service.HaalOverzichtOp();

        Assert.Empty(result);
    }

    [Fact]
    public void HaalOverzichtOp_InactieveAbonnementenWordenGefilterd()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        List<Abonnement> result = service.HaalOverzichtOp();

        Assert.All(result, a => Assert.True(a.IsActief));
    }
}
