using Interface.Dtos;
using Interface.Models;
using Interface.Repositories;
using Interface.Services;

namespace Logic.Services;

public class AbonnementService : IAbonnementService
{
    private readonly IAbonnementRepository _abonnementRepository;

    public AbonnementService(IAbonnementRepository abonnementRepository)
    {
        _abonnementRepository = abonnementRepository;
    }

    public List<AbonnementDto> HaalOverzichtOp(string? provider = null)
    {
        return _abonnementRepository.HaalActieveAbonnementenOp()
            .Where(a => provider == null || a.Provider == provider)
            .Select(a => new AbonnementDto
            {
                Id = a.Id,
                Naam = a.Naam,
                Provider = a.Provider,
                PrijsPerMaand = a.PrijsPerMaand,
                Beschrijving = a.Beschrijving
            })
            .ToList();
    }
}

