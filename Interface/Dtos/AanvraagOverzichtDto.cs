namespace Interface.Dtos;

public class AanvraagOverzichtDto
{
    public int Id { get; set; } // namen komen van meerdere entiteiten 
    public required string KlantNaam { get; set; }
    public required string KlantEmail { get; set; } // dit is voor wat er straks allemaal op het overzciht tentoongesteld moet worden
    public required string AbonnementNaam { get; set; }
    public DateTime AanvraagDatum { get; set; }
    public required string Status { get; set; }
    public string Provider { get; set; } = string.Empty;
}