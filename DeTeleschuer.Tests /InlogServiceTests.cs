using DeTeleschuer.Tests.Fakes;
using Logic.Services;

namespace DeTeleschuer.Tests;

public class InlogServiceTests
{
    [Fact]
    public void ControleerInloggegevens_MetJuisteGegevens_GeeftTrueTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());
        Assert.True(service.ControleerInloggegevens("admin", "geheim123"));
    }

    [Fact]
    public void ControleerInloggegevens_MetVerkerdWachtwoord_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());
        Assert.False(service.ControleerInloggegevens("admin", "foutWachtwoord"));
    }

    [Fact]
    public void ControleerInloggegevens_MetOnbekendeGebruiker_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());
        Assert.False(service.ControleerInloggegevens("onbekend", "geheim123"));
    }

    [Fact]
    public void RegistreerGebruiker_NieuweGebruiker_KanDaarnaaInloggen()
    {
        var repo = new FakeGebruikerRepository();
        var service = new InlogService(repo);
        service.RegistreerGebruiker("nieuw", "wachtwoord99");
        Assert.True(service.ControleerInloggegevens("nieuw", "wachtwoord99"));
    }
}
