using Interface.Models;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeLegeAbonnementRepository : IAbonnementRepository
{
    public List<Abonnement> HaalActieveAbonnementenOp() => [];
}
