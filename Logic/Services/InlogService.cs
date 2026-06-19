using Interface.Models;
using Interface.Repositories;
using Logic.Mappers;

namespace Logic.Services;

public class InlogService
{
    private readonly IGebruikerRepository _gebruikerRepository;

    public InlogService(IGebruikerRepository gebruikerRepository)
    {
        _gebruikerRepository = gebruikerRepository;
    }

    public bool ControleerInloggegevens(string gebruikersnaam, string wachtwoord)
    {
        var dto = _gebruikerRepository.HaalOpViaGebruikersnaam(gebruikersnaam);
        if (dto == null) return false;
        var model = GebruikerMapper.NaarModel(dto);
        return BCrypt.Net.BCrypt.Verify(wachtwoord, model.WachtwoordHash);
    }

    public void RegistreerGebruiker(string gebruikersnaam, string wachtwoord)
    {
        var hash = BCrypt.Net.BCrypt.HashPassword(wachtwoord);
        var model = new Gebruiker(gebruikersnaam, hash);
        _gebruikerRepository.Aanmaken(GebruikerMapper.NaarDto(model));
    }
}
