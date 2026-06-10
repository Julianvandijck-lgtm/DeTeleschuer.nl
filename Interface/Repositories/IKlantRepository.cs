using Interface.Models;

namespace Interface.Repositories;

public interface IKlantRepository
{
    int Opslaan(Klant klant);
    int? HaalIdOpViaEmail(string email);
}