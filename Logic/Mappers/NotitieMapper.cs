using Interface.Dtos;
using Interface.Models;

namespace Logic.Mappers;

public static class NotitieMapper
{
    public static Notitie NaarModel(NotitieDto dto)
    {
        return new Notitie(
            aanvraagId: dto.AanvraagId,
            tekst: dto.Tekst,
            datumAangemaakt: dto.DatumAangemaakt,
            id: dto.Id
        );
    }

    public static NotitieDto NaarDto(Notitie model)
    {
        return new NotitieDto
        {
            Id = model.Id,
            AanvraagId = model.AanvraagId,
            Tekst = model.Tekst,
            DatumAangemaakt = model.DatumAangemaakt
        };
    }
}
