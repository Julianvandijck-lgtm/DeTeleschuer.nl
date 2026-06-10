using Interface.Models;

namespace Interface.Services;

public interface IAanvraagService
{
    void AanvraagOpslaan(Klant klant, Aanvraag aanvraag); // klant en aanvraag zijn een aparte entiteit en worden apart opgeslagen met hun eigen object maar het word later in het overzicht wel een geheel
}