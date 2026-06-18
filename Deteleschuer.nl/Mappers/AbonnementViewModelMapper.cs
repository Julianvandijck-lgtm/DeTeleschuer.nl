using Interface.Enums;
using Interface.Models;
using Deteleschuer.nl.ViewModels;

namespace Deteleschuer.nl.Mappers;

public class AbonnementViewModelMapper
{
    public AbonnementViewModel NaarViewModel(Abonnement model)
    {
        var (tekst, cssKlasse, logoSrc) = model.Provider switch
        {
            Provider.Vodafone => ("Vodafone", "card--vodafone", "~/img/vodafone.png"),
            Provider.Odido    => ("Odido",    "card--odido",    "~/img/odido.webp"),
            _                 => ("Onbekend", "",               "")
        };

        return new AbonnementViewModel
        {
            Id             = model.Id,
            Naam           = model.Naam,
            ProviderTekst  = tekst,
            ProviderCssKlasse = cssKlasse,
            ProviderLogoSrc   = logoSrc,
            PrijsPerMaand  = model.PrijsPerMaand,
            Beschrijving   = model.Beschrijving
        };
    }
}
