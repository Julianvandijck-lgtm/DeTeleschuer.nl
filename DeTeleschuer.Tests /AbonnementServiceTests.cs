using DeTeleschuer.Tests.Fakes;
using Logic.Services;

namespace DeTeleschuer.Tests;

public class AbonnementServiceTests
{
    [Fact]
    public void HaalOverzichtOp_ZonderFilter_GeeftAlleAbonnementenTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        try
        {
            var result = service.HaalOverzichtOp();
            Assert.Equal(3, result.Count);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void HaalOverzichtOp_MetProviderFilter_GeeftAlleenAbonnementenVanDieProviderTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        try
        {
            var result = service.HaalOverzichtOp("Odido");
            Assert.Equal(2, result.Count);
            Assert.All(result, a => Assert.Equal("Odido", a.Provider));
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void HaalOverzichtOp_MetOnbekendeProvider_GeeftLegeListTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        try
        {
            var result = service.HaalOverzichtOp("T-Mobile");
            Assert.Empty(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void HaalOverzichtOp_GeeftDtoTerug_MetJuisteVelden()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        try
        {
            var dto = service.HaalOverzichtOp().First();
            Assert.Equal(1, dto.Id);
            Assert.Equal("Basis", dto.Naam);
            Assert.Equal("Odido", dto.Provider);
            Assert.Equal(10m, dto.PrijsPerMaand);
            Assert.Equal("Basis pakket", dto.Beschrijving);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void HaalOverzichtOp_LegeFakeRepository_GeeftLegeListTerug()
    {
        var service = new AbonnementService(new FakeLegeAbonnementRepository());

        try
        {
            var result = service.HaalOverzichtOp();
            Assert.Empty(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void HaalOverzichtOp_FilterIsHoofdlettergevoelig_GeeftLegeListTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        try
        {
            var result = service.HaalOverzichtOp("odido");
            Assert.Empty(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void HaalOverzichtOp_MetLegeStringAlsProvider_GeeftLegeListTerug()
    {
        var service = new AbonnementService(new FakeAbonnementRepository());

        try
        {
            var result = service.HaalOverzichtOp("");
            Assert.Empty(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
