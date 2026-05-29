using Interface.Models;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeGebruikerRepository : IGebruikerRepository
{
    private static readonly string AdminHash = BCrypt.Net.BCrypt.HashPassword("geheim123");

    private readonly List<Gebruiker> _gebruikers =
    [
        new() { Id = 1, Gebruikersnaam = "admin", WachtwoordHash = AdminHash }
    ];

    public Gebruiker? HaalOpViaGebruikersnaam(string gebruikersnaam) =>
        _gebruikers.FirstOrDefault(g => g.Gebruikersnaam == gebruikersnaam);

    public void Aanmaken(string gebruikersnaam, string wachtwoordHash)
    {
        _gebruikers.Add(new Gebruiker { Gebruikersnaam = gebruikersnaam, WachtwoordHash = wachtwoordHash });
    }
}
