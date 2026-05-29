using DeTeleschuer.Tests.Fakes;
using Logic.Services;

namespace DeTeleschuer.Tests;

public class InlogServiceTests
{
    [Fact]
    public void ControleerInloggegevens_MetJuisteGegevens_GeeftTrueTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());

        try
        {
            var result = service.ControleerInloggegevens("admin", "geheim123");
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void ControleerInloggegevens_MetVerkerdWachtwoord_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());

        try
        {
            var result = service.ControleerInloggegevens("admin", "foutWachtwoord");
            Assert.False(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void ControleerInloggegevens_MetOnbekendeGebruiker_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());

        try
        {
            var result = service.ControleerInloggegevens("onbekend", "geheim123");
            Assert.False(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void ControleerInloggegevens_MetLegeGebruikersnaam_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());

        try
        {
            var result = service.ControleerInloggegevens("", "geheim123");
            Assert.False(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void ControleerInloggegevens_MetLeegWachtwoord_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());

        try
        {
            var result = service.ControleerInloggegevens("admin", "");
            Assert.False(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }

    [Fact]
    public void ControleerInloggegevens_GebruikersnaamIsHoofdlettergevoelig_GeeftFalseTerug()
    {
        var service = new InlogService(new FakeGebruikerRepository());

        try
        {
            var result = service.ControleerInloggegevens("Admin", "geheim123");
            Assert.False(result);
        }
        catch (Exception ex)
        {
            Assert.Fail(ex.Message);
        }
    }
}
