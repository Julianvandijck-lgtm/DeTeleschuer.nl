namespace Interface.Models;

public class Klant
{
    public int Id { get; }
    public string Naam { get; }
    public string Straatnaam { get; }
    public string Huisnummer { get; }
    public DateOnly GeboorteDatum { get; }
    public string Email { get; }
    public string Telefoon { get; }
    public string FotoID { get; }
    public string FotoBankpas { get; }

    public Klant(string naam, string straatnaam, string huisnummer, DateOnly geboorteDatum, string email, string telefoon, string fotoId, string fotoBankpas, int id = 0)
    {
        if (string.IsNullOrWhiteSpace(naam))
            throw new ArgumentException("Naam mag niet leeg zijn.", nameof(naam));
        if (string.IsNullOrWhiteSpace(straatnaam))
            throw new ArgumentException("Straatnaam mag niet leeg zijn.", nameof(straatnaam));
        if (string.IsNullOrWhiteSpace(huisnummer))
            throw new ArgumentException("Huisnummer mag niet leeg zijn.", nameof(huisnummer));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Ongeldig e-mailadres.", nameof(email));
        if (string.IsNullOrWhiteSpace(telefoon))
            throw new ArgumentException("Telefoon mag niet leeg zijn.", nameof(telefoon));
        if (geboorteDatum >= DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Geboortedatum mag niet in de toekomst liggen.", nameof(geboorteDatum));

        Naam = naam;
        Straatnaam = straatnaam;
        Huisnummer = huisnummer;
        GeboorteDatum = geboorteDatum;
        Email = email;
        Telefoon = telefoon;
        FotoID = fotoId;
        FotoBankpas = fotoBankpas;
        Id = id;
    }
}