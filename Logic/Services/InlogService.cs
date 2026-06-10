using BCrypt.Net;
using Interface.Repositories;
using Interface.Services;

namespace Logic.Services;

public class InlogService : IInlogService
{
    private readonly IGebruikerRepository _gebruikerRepository;

    public InlogService(IGebruikerRepository gebruikerRepository)
    {
        _gebruikerRepository = gebruikerRepository;
    }

    public bool ControleerInloggegevens(string gebruikersnaam, string wachtwoord)
    {
        var gebruiker = _gebruikerRepository.HaalOpViaGebruikersnaam(gebruikersnaam);
        if (gebruiker == null) return false;
        return BCrypt.Net.BCrypt.Verify(wachtwoord, gebruiker.WachtwoordHash); // ik maak van mijn ww een hash met Bcrypt dus hij maakt van mijn ww een hele lange salt zodat niemand hem kan kraken 
    }// ik kan er nu zelf ook niet meer bij dus moet hem goed onthouden 
}