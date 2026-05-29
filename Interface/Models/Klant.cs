namespace Interface.Models;


public class Klant
{
    public int Id { get; set; }
    public required string Naam { get; set; }
    public required string Adres { get; set; }
    public required DateOnly GeboorteDatum { get; set; }
    public required string Email { get; set; }
    public required string Telefoon { get; set; }
    public required string FotoID { get; set; }
    public required string FotoBankpas { get; set; }
}