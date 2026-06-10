using Interface.Dtos; // De eindgebruiker hoeft helemaal niet te zien of een abo de status acief heeft, word eruit gefilterd!

namespace Interface.Services;

public interface IAbonnementService
{
    List<AbonnementDto> HaalOverzichtOp(string? provider = null); 
}

// met string? provider = nul  maak ik de filter want als ik provider waarde nul invul dus niks geef ie alle actieve abonnementen terug
// in de logic ga ik pas valideren


