using Interface.Models;

namespace Interface.Services;

public interface IAanvraagService
{
    void AanvraagOpslaan(Klant klant, Aanvraag aanvraag);
}