using Interface.Enums;
using Interface.Models;
using Interface.Repositories;
using Logic.Mappers;

namespace Logic.Services;

public class AbonnementService
{
    private readonly IAbonnementRepository _abonnementRepository;
    private readonly AbonnementMapper _mapper = new();

    public AbonnementService(IAbonnementRepository abonnementRepository)
    {
        _abonnementRepository = abonnementRepository;
    }

    public List<Abonnement> HaalOverzichtOp(Provider? provider = null)
    {
        return _abonnementRepository.HaalAlleOp()
            .Select(_mapper.NaarModel)
            .Where(a => a.IsActief)
            .Where(a => provider == null || a.Provider == provider)
            .ToList();
    }
}

