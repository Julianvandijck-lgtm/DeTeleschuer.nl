using Interface.Dtos;
using Interface.Models;
using Interface.Repositories;
using Logic.Mappers;

namespace Logic.Services;

public class NotitieService
{
    private readonly INotitieRepository _notitieRepository;

    public NotitieService(INotitieRepository notitieRepository)
    {
        _notitieRepository = notitieRepository;
    }

    public List<NotitieDto> HaalVoorAanvraag(int aanvraagId) =>
        _notitieRepository.HaalOpVoorAanvraag(aanvraagId);

    public void Toevoegen(int aanvraagId, string tekst)
    {
        var model = new Notitie(aanvraagId, tekst, DateTime.Now);
        _notitieRepository.Opslaan(NotitieMapper.NaarDto(model));
    }

    public void Bijwerken(int id, string tekst)
    {
        if (string.IsNullOrWhiteSpace(tekst))
            throw new ArgumentException("Tekst mag niet leeg zijn.", nameof(tekst));
        _notitieRepository.Bijwerken(id, tekst);
    }
}
