using Interface.Dtos;

namespace Interface.Services;

public interface IAbonnementService
{
    List<AbonnementDto> HaalOverzichtOp(string? provider = null);
}





