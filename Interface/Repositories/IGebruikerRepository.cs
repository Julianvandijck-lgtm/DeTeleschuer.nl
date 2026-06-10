using Interface.Models;

namespace Interface.Repositories;

public interface IGebruikerRepository
{
    Gebruiker? HaalOpViaGebruikersnaam(string gebruikersnaam);
    void Aanmaken(string gebruikersnaam, string wachtwoordHash); // hash word gemaakt in service 
}