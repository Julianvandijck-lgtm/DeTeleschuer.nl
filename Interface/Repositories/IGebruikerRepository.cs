using Interface.Dtos;

namespace Interface.Repositories;

public interface IGebruikerRepository
{
    GebruikerDto? HaalOpViaGebruikersnaam(string gebruikersnaam);
    void Aanmaken(GebruikerDto gebruiker);
}
