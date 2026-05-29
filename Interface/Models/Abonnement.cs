namespace Interface.Models;


public class Abonnement
{
    public int Id { get; set; }
    public required string Naam { get; set; }
    public required string Provider { get; set; }
    public decimal PrijsPerMaand {get; set;}
    public bool IsActief { get; set; }
    public required string Beschrijving { get; set; }
}
