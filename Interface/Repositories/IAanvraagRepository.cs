using Interface.Dtos;
using Interface.Models;

namespace Interface.Repositories;

public interface IAanvraagRepository
{
    void Opslaan(Aanvraag aanvraag);
    List<AanvraagOverzichtDto> HaalAlleOp();
    
    AanvraagDetailDto? HaalDetailOp(int id);
    void StatusBijwerken(int id, string status);
}
