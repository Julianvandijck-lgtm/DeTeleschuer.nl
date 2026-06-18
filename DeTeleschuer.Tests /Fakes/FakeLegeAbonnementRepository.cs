using Interface.Dtos;
using Interface.Repositories;

namespace DeTeleschuer.Tests.Fakes;

public class FakeLegeAbonnementRepository : IAbonnementRepository
{
    public List<AbonnementDto> HaalAlleOp() => [];
}
