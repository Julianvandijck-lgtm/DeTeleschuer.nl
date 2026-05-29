using Interface.Models;
using Interface.Repositories;
using Interface.Services;

namespace Logic.Services;

public class AanvraagService : IAanvraagService
{
    private readonly IKlantRepository _klantRepository;
    private readonly IAanvraagRepository _aanvraagRepository;

    public AanvraagService(IKlantRepository klantRepository, IAanvraagRepository aanvraagRepository)
    {
        _klantRepository = klantRepository;
        _aanvraagRepository = aanvraagRepository;
    }

    public void AanvraagOpslaan(Klant klant, Aanvraag aanvraag)
    {
        var klantId = _klantRepository.Opslaan(klant);
        aanvraag.KlantId = klantId;
        _aanvraagRepository.Opslaan(aanvraag);
    }
}