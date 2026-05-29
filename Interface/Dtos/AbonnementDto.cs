namespace Interface.Dtos;

public class AbonnementDto
{
    public int Id { get; set; }
    public required string Naam { get; set; }
    public required string Provider { get; set; }
    public decimal PrijsPerMaand { get; set; }
    public required string Beschrijving { get; set; }
}