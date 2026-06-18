namespace Interface.Models;

public class Klant
{
    public int Id { get; }
    public string Naam { get; }
    public string Adres { get; }
    public DateOnly GeboorteDatum { get; }
    public string Email { get; }
    public string Telefoon { get; }
    public string FotoID { get; }
    public string FotoBankpas { get; }

    public Klant(string naam, string adres, DateOnly geboorteDatum, string email, string telefoon, string fotoId, string fotoBankpas, int id = 0)
    {
        Naam = naam;
        Adres = adres;
        GeboorteDatum = geboorteDatum;
        Email = email;
        Telefoon = telefoon;
        FotoID = fotoId;
        FotoBankpas = fotoBankpas;
        Id = id;
    }
}