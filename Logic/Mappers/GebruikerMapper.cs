using Interface.Dtos;
using Interface.Models;

namespace Logic.Mappers;

public static class GebruikerMapper
{
    public static Gebruiker NaarModel(GebruikerDto dto)
    {
        return new Gebruiker(
            gebruikersnaam: dto.Gebruikersnaam,
            wachtwoordHash: dto.WachtwoordHash,
            id: dto.Id
        );
    }

    public static GebruikerDto NaarDto(Gebruiker model)
    {
        return new GebruikerDto
        {
            Id = model.Id,
            Gebruikersnaam = model.Gebruikersnaam,
            WachtwoordHash = model.WachtwoordHash
        };
    }
}
