namespace Interface.Models;

public class Gebruiker
{
    public int Id { get; }
    public string Gebruikersnaam { get; }
    public string WachtwoordHash { get; }

    public Gebruiker(string gebruikersnaam, string wachtwoordHash, int id = 0)
    {
        if (string.IsNullOrWhiteSpace(gebruikersnaam))
            throw new ArgumentException("Gebruikersnaam mag niet leeg zijn.", nameof(gebruikersnaam));
        if (string.IsNullOrWhiteSpace(wachtwoordHash))
            throw new ArgumentException("WachtwoordHash mag niet leeg zijn.", nameof(wachtwoordHash));

        Id = id;
        Gebruikersnaam = gebruikersnaam;
        WachtwoordHash = wachtwoordHash;
    }
}
