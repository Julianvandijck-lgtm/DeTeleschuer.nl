using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeAbonnementRepository : IAbonnementRepository
{
    public List<AbonnementDto> HaalAlleOp() =>
    [
        new AbonnementDto { Id = 1, Naam = "Basis",   Provider = "Odido",    PrijsPerMaand = 10m, IsActief = true,  Beschrijving = "Basis pakket"   },
        new AbonnementDto { Id = 2, Naam = "Premium", Provider = "Odido",    PrijsPerMaand = 25m, IsActief = true,  Beschrijving = "Premium pakket" },
        new AbonnementDto { Id = 3, Naam = "Start",   Provider = "Vodafone", PrijsPerMaand = 15m, IsActief = true,  Beschrijving = "Start pakket"   },
        new AbonnementDto { Id = 4, Naam = "Oud",     Provider = "Odido",    PrijsPerMaand = 5m,  IsActief = false, Beschrijving = "Oud pakket"     }
    ];
}
