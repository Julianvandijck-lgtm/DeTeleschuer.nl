using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeGebruikerRepository : IGebruikerRepository
{
    private static readonly string AdminHash = BCrypt.Net.BCrypt.HashPassword("geheim123");

    private readonly List<GebruikerDto> _gebruikers =
    [
        new GebruikerDto { Id = 1, Gebruikersnaam = "admin", WachtwoordHash = AdminHash }
    ];

    public GebruikerDto? HaalOpViaGebruikersnaam(string gebruikersnaam) =>
        _gebruikers.FirstOrDefault(g => g.Gebruikersnaam == gebruikersnaam);

    public void Aanmaken(GebruikerDto gebruiker)
    {
        _gebruikers.Add(gebruiker);
    }
}
