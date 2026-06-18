using Interface.Dtos;
using Interface.Enums;
using Interface.Models;

namespace Logic.Mappers;

public class AbonnementMapper
{
    public Abonnement NaarModel(AbonnementDto dto)
    {
        if (!Enum.TryParse<Provider>(dto.Provider, out var provider))
            throw new ArgumentException($"Onbekende provider waarde '{dto.Provider}' in de database.", nameof(dto));

        return new Abonnement(
            id: dto.Id,
            naam: dto.Naam,
            provider: provider,
            prijsPerMaand: dto.PrijsPerMaand,
            beschrijving: dto.Beschrijving,
            isActief: dto.IsActief
        );
    }

    public AbonnementDto NaarDto(Abonnement model)
    {
        return new AbonnementDto
        {
            Id = model.Id,
            Naam = model.Naam,
            Provider = model.Provider.ToString(),
            PrijsPerMaand = model.PrijsPerMaand,
            IsActief = model.IsActief,
            Beschrijving = model.Beschrijving
        };
    }
}
