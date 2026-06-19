using Interface.Dtos;

namespace Interface.Repositories;

public interface IKlantRepository
{
    int Opslaan(KlantDto klant);
    int? HaalIdOpViaEmail(string email);
}
