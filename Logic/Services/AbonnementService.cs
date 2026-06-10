using Interface.Dtos;
using Interface.Models;
using Interface.Repositories;
using Interface.Services;

namespace Logic.Services;

public class AbonnementService : IAbonnementService
{
    private readonly IAbonnementRepository _abonnementRepository; // zodat hij niet gebonden is aan concrete implementatie

    public AbonnementService(IAbonnementRepository abonnementRepository)
    {
        _abonnementRepository = abonnementRepository;
    }
// ik roep dto aan zodat de homecontroller alleen krijgt wat nodig is
    public List<AbonnementDto> HaalOverzichtOp(string? provider = null) //filter systeem null = alle abonnementen tonen
    { // ? zodat het mogelijk is om niks in te vullen
        return _abonnementRepository.HaalActieveAbonnementenOp()// a staat voor abonnement 
            .Where(a => provider == null || a.Provider == provider)// || staat voor of dus als niks wword ingevuld hoeft hij de andere kant niet eens te checken
            .Select(a => new AbonnementDto // select maak voor elk abonnemt een nieuwe dto aan met de juiste waarde hieronder
            {
                Id = a.Id,
                Naam = a.Naam,
                Provider = a.Provider, 
                PrijsPerMaand = a.PrijsPerMaand,
                Beschrijving = a.Beschrijving
            })//
            .ToList();// where en select worden daadwerkelijk uitgevoerd en word opgeslageen in een lijst in het geheugen
    }
}

