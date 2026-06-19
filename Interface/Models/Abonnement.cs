using Interface.Enums;

namespace Interface.Models;

public class Abonnement
{
    public int Id { get; }
    public string Naam { get; }
    public Provider Provider { get; }
    public decimal PrijsPerMaand { get; }
    public bool IsActief { get; }
    public string Beschrijving { get; }

    public Abonnement(int id, string naam, Provider provider, decimal prijsPerMaand, bool isActief, string beschrijving)
    {
        if (string.IsNullOrWhiteSpace(naam))
            throw new ArgumentException("Naam mag niet leeg zijn.", nameof(naam));
        if (string.IsNullOrWhiteSpace(beschrijving))
            throw new ArgumentException("Beschrijving mag niet leeg zijn.", nameof(beschrijving));
        if (prijsPerMaand < 0)
            throw new ArgumentException("PrijsPerMaand mag niet negatief zijn.", nameof(prijsPerMaand));
        if (!Enum.IsDefined(typeof(Provider), provider))
            throw new ArgumentException("Ongeldige provider opgegeven.", nameof(provider));

        Id = id;
        Naam = naam;
        Provider = provider;
        PrijsPerMaand = prijsPerMaand;
        IsActief = isActief;
        Beschrijving = beschrijving;
    }
}