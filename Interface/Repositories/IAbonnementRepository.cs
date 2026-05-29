using Interface.Models;

namespace Interface.Repositories;

public interface IAbonnementRepository

{
    List<Abonnement> HaalActieveAbonnementenOp();
}