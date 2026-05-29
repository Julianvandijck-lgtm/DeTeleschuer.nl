using Interface.Models;

namespace Interface.Repositories;

public interface INotitieRepository
{
    List<Notitie> HaalOpVoorAanvraag(int aanvraagId);
    void Opslaan(Notitie notitie);
    void Bijwerken(Notitie notitie);
}