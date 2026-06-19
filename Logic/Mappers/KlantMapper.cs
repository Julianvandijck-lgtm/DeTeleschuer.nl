using Interface.Dtos;
using Interface.Models;

namespace Logic.Mappers;

public static class KlantMapper
{
    public static KlantDto NaarDto(Klant model)
    {
        return new KlantDto
        {
            Id = model.Id,
            Naam = model.Naam,
            Straatnaam = model.Straatnaam,
            Huisnummer = model.Huisnummer,
            GeboorteDatum = model.GeboorteDatum,
            Email = model.Email,
            Telefoon = model.Telefoon,
            FotoID = model.FotoID,
            FotoBankpas = model.FotoBankpas
        };
    }

    public static Klant NaarModel(KlantDto dto)
    {
        return new Klant(
            naam: dto.Naam,
            straatnaam: dto.Straatnaam,
            huisnummer: dto.Huisnummer,
            geboorteDatum: dto.GeboorteDatum,
            email: dto.Email,
            telefoon: dto.Telefoon,
            fotoId: dto.FotoID,
            fotoBankpas: dto.FotoBankpas,
            id: dto.Id
        );
    }
}
