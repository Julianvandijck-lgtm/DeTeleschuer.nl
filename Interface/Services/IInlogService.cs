namespace Interface.Services;

public interface IInlogService
{
    bool ControleerInloggegevens(string gebruikersnaam, string wachtwoord);
}