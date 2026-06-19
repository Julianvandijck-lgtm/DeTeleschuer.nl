using Interface.Dtos;
using Interface.Enums;
using Interface.Models;
using Interface.Repositories;
using Logic.Mappers;

namespace Logic.Services;

public class AanvraagService
{
    private readonly IKlantRepository _klantRepository;
    private readonly IAanvraagRepository _aanvraagRepository;

    public AanvraagService(IKlantRepository klantRepository, IAanvraagRepository aanvraagRepository)
    {
        _klantRepository = klantRepository;
        _aanvraagRepository = aanvraagRepository;
    }

    public void AanvraagOpslaan(Klant klant, int abonnementId, bool? nummerBehouden, string digitaleHandtekening)
    {
        var bestaandId = _klantRepository.HaalIdOpViaEmail(klant.Email);
        var klantId = bestaandId ?? _klantRepository.Opslaan(KlantMapper.NaarDto(klant));

        var aanvraag = new Aanvraag(
            id: 0,
            klantId: klantId,
            abonnementId: abonnementId,
            aanvraagDatum: DateTime.Now,
            status: AanvraagStatus.Nieuw,
            nummerBehouden: nummerBehouden,
            digitaleHandtekening: digitaleHandtekening,
            handtekeningDatum: DateTime.Now
        );

        _aanvraagRepository.Opslaan(AanvraagMapper.NaarDto(aanvraag));
    }

    public List<AanvraagOverzichtDto> HaalOverzicht() => _aanvraagRepository.HaalAlleOp();

    public AanvraagDetailDto? HaalDetail(int id) => _aanvraagRepository.HaalDetailOp(id);

    public void WerkStatusBij(int id, AanvraagStatus status) =>
        _aanvraagRepository.StatusBijwerken(id, AanvraagMapper.NaarStatusString(status));
}
