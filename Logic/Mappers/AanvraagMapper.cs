using Interface.Dtos;
using Interface.Enums;
using Interface.Models;

namespace Logic.Mappers;

public static class AanvraagMapper
{
    public static AanvraagDto NaarDto(Aanvraag model)
    {
        return new AanvraagDto
        {
            Id = model.Id,
            KlantId = model.KlantId,
            AbonnementId = model.AbonnementId,
            AanvraagDatum = model.AanvraagDatum,
            Status = NaarStatusString(model.Status),
            NummerBehouden = model.NummerBehouden,
            DigitaleHandtekening = model.DigitaleHandtekening,
            HandtekeningDatum = model.HandtekeningDatum
        };
    }

    public static Aanvraag NaarModel(AanvraagDto dto)
    {
        return new Aanvraag(
            id: dto.Id,
            klantId: dto.KlantId,
            abonnementId: dto.AbonnementId,
            aanvraagDatum: dto.AanvraagDatum,
            status: NaarStatusEnum(dto.Status),
            nummerBehouden: dto.NummerBehouden,
            digitaleHandtekening: dto.DigitaleHandtekening,
            handtekeningDatum: dto.HandtekeningDatum
        );
    }

    public static string NaarStatusString(AanvraagStatus status) => status switch
    {
        AanvraagStatus.Nieuw          => "Nieuw",
        AanvraagStatus.InBehandeling  => "In behandeling",
        AanvraagStatus.Afgerond       => "Afgerond",
        _ => throw new ArgumentException($"Onbekende aanvraagstatus: {status}", nameof(status))
    };

    public static AanvraagStatus NaarStatusEnum(string status) => status switch
    {
        "Nieuw"          => AanvraagStatus.Nieuw,
        "In behandeling" => AanvraagStatus.InBehandeling,
        "Afgerond"       => AanvraagStatus.Afgerond,
        _ => throw new ArgumentException($"Onbekende statuswaarde '{status}' in de database.", nameof(status))
    };
}
