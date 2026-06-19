using DeTeleschuer.Tests.Fakes;
using Logic.Services;

namespace DeTeleschuer.Tests;

public class NotitieServiceTests
{
    [Fact]
    public void Toevoegen_RoeptRepositoryAan_EnZetDatumAangemaakt()
    {
        var repo = new FakeNotitieRepository();
        var service = new NotitieService(repo);
        var voor = DateTime.Now;

        service.Toevoegen(1, "Testnotitie");

        Assert.Single(repo.OpgeslagenNotities);
        var opgeslagen = repo.OpgeslagenNotities[0];
        Assert.Equal(1, opgeslagen.AanvraagId);
        Assert.Equal("Testnotitie", opgeslagen.Tekst);
        Assert.True(opgeslagen.DatumAangemaakt >= voor);
    }

    [Fact]
    public void Bijwerken_MetLegeTekst_GooidArgumentException()
    {
        var service = new NotitieService(new FakeNotitieRepository());
        Assert.Throws<ArgumentException>(() => service.Bijwerken(1, ""));
    }

    [Fact]
    public void Bijwerken_MetGeldigeTekst_RoeptRepositoryAan()
    {
        var repo = new FakeNotitieRepository();
        var service = new NotitieService(repo);

        service.Bijwerken(5, "Aangepaste tekst");

        Assert.Equal(5, repo.BijgewerktId);
        Assert.Equal("Aangepaste tekst", repo.BijgewerktTekst);
    }
}
