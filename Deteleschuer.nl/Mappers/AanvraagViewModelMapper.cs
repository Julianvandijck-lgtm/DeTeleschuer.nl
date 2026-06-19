using Interface.Dtos;
using Deteleschuer.nl.ViewModels;

namespace Deteleschuer.nl.Mappers;

public static class AanvraagViewModelMapper
{
    public static AanvraagRegelViewModel NaarRegelViewModel(AanvraagOverzichtDto dto)
    {
        return new AanvraagRegelViewModel
        {
            Id = dto.Id,
            KlantNaam = dto.KlantNaam,
            KlantEmail = dto.KlantEmail,
            AbonnementNaam = dto.AbonnementNaam,
            AanvraagDatum = dto.AanvraagDatum,
            StatusTekst = dto.Status,
            Provider = dto.Provider
        };
    }

    public static AanvraagDetailViewModel NaarDetailViewModel(AanvraagDetailDto dto, List<NotitieDto> notities)
    {
        return new AanvraagDetailViewModel
        {
            Id = dto.Id,
            KlantNaam = dto.KlantNaam,
            KlantStraatnaam = dto.KlantStraatnaam,
            KlantHuisnummer = dto.KlantHuisnummer,
            KlantGeboortedatum = dto.KlantGeboortedatum,
            KlantEmail = dto.KlantEmail,
            KlantTelefoon = dto.KlantTelefoon,
            FotoLegitimatie = dto.FotoLegitimatie,
            FotoBankpas = dto.FotoBankpas,
            AbonnementNaam = dto.AbonnementNaam,
            AanvraagDatum = dto.AanvraagDatum,
            StatusTekst = dto.Status,
            NummerBehouden = dto.NummerBehouden,
            DigitaleHandtekening = dto.DigitaleHandtekening,
            Notities = notities.Select(n => new NotitieViewModel
            {
                Id = n.Id,
                AanvraagId = n.AanvraagId,
                Tekst = n.Tekst,
                DatumAangemaakt = n.DatumAangemaakt
            }).ToList()
        };
    }
}
