using Interface.Models;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeAbonnementRepository : IAbonnementRepository // ik gebruik Iabonnement repository omdat de service deze verwacht maakt niet uit of ie fake is 
{
    public List<Abonnement> HaalActieveAbonnementenOp() =>
    [
        new() { Id = 1, Naam = "Basis",   Provider = "Odido",    PrijsPerMaand = 10m, IsActief = true, Beschrijving = "Basis pakket"   },
        new() { Id = 2, Naam = "Premium", Provider = "Odido",    PrijsPerMaand = 25m, IsActief = true, Beschrijving = "Premium pakket" },
        new() { Id = 3, Naam = "Start",   Provider = "Vodafone", PrijsPerMaand = 15m, IsActief = true, Beschrijving = "Start pakket"   }
    ];
}
