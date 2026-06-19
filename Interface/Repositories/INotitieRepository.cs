using Interface.Dtos;

namespace Interface.Repositories;

public interface INotitieRepository
{
    List<NotitieDto> HaalOpVoorAanvraag(int aanvraagId);
    void Opslaan(NotitieDto notitie);
    void Bijwerken(int id, string tekst);
}
