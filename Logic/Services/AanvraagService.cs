using Interface.Models;
using Interface.Repositories;
using Interface.Services;

namespace Logic.Services;

public class AanvraagService : IAanvraagService
{
    private readonly IKlantRepository _klantRepository;
    private readonly IAanvraagRepository _aanvraagRepository; // 2 repositorys omdat we te maken hebben met 2 aparte entiteiten in een proces 

    public AanvraagService(IKlantRepository klantRepository, IAanvraagRepository aanvraagRepository)
    {
        _klantRepository = klantRepository;
        _aanvraagRepository = aanvraagRepository;
    }

    public void AanvraagOpslaan(Klant klant, Aanvraag aanvraag)
    {
        var bestaandId = _klantRepository.HaalIdOpViaEmail(klant.Email);
        var klantId = bestaandId ?? _klantRepository.Opslaan(klant); // bestaat klant al? hergebruik klant id anders nieuw aanmaken
        aanvraag.KlantId = klantId;
        _aanvraagRepository.Opslaan(aanvraag);
    }
}

// je moet klant opslaan wil je een aanrvaag kunnen opslaan want daar staan alle hgegevens in en met die klant id geef je dus die gegevens mee aan de aanvraag
// geen klant id in de aanvraag is geen insert 
// hij onthoud klant gegevens aan Email zodra hij email herkent worden de oude gegevens direct overgenomen: klantgegevens en documenten. string abonnement en die andere horen allemaal bij aanvraag dus doe worden wel mee aangepast 