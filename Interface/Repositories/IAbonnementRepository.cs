using Interface.Models;

namespace Interface.Repositories;

public interface IAbonnementRepository

{
    List<Abonnement> HaalActieveAbonnementenOp();  // List gebruikt omdat ik meerdere abonnementen moet terug geven niet eentje.
} 

// actieve abonnementen zodat eerst meteen gefilterd word op alleen actieve abonnementen. 