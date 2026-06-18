namespace Interface.Dtos;

public class AbonnementDto
{
    public int Id { get; set; }
    public string Naam { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public decimal PrijsPerMaand { get; set; }
    public bool IsActief { get; set; }
    public string Beschrijving { get; set; } = string.Empty;
}