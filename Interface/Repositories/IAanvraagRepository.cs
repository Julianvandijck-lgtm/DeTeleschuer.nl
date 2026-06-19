using Interface.Dtos;

namespace Interface.Repositories;

public interface IAanvraagRepository
{
    void Opslaan(AanvraagDto aanvraag);
    List<AanvraagOverzichtDto> HaalAlleOp();
    AanvraagDetailDto? HaalDetailOp(int id);
    void StatusBijwerken(int id, string status);
}
