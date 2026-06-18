using Interface.Dtos;

namespace Interface.Repositories;

public interface IAbonnementRepository
{
    List<AbonnementDto> HaalAlleOp();
}