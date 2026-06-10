using Interface.Dtos;
using Interface.Models;

namespace Interface.Repositories;

public interface IAanvraagRepository
{
    void Opslaan(Aanvraag aanvraag); // ik hoef niks op te halen bij de database maar jusit schrijven naar daarom void opslaan
    List<AanvraagOverzichtDto> HaalAlleOp(); // Het hele aanvraag object meegeven want ik sla het gehele pakket in een keer op geen tussen opslaan 
    
    AanvraagDetailDto? HaalDetailOp(int id); // ook een aparte detail dto want hierop wil ik alle gegevens laten zien en op de aanvragen maar een paar zodat de gebruiker de juiste aanvraag kan vinden en als je er dan op klikt dat ie alles kan inzien dus 2 zodat het onderscheiden word van elkaar 
    void StatusBijwerken(int id, string status); // ik heb alleen een id nodig zodat het systeem weet bij welke aanvraag de status hoort
}
