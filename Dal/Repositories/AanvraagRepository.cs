using Interface.Models;
using Interface.Repositories;
using Microsoft.Data.SqlClient;
using Interface.Dtos;

namespace Dal.Repositories;

public class AanvraagRepository : IAanvraagRepository
{
    private readonly string _connectionString;

    public AanvraagRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Opslaan(Aanvraag aanvraag)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = @"INSERT INTO Aanvraag (KlantID, AbonnementID, AanvraagDatum, Status, NummerBehouden, NieuwNummer, DigitaleHandtekening, HandtekeningDatum)
                    VALUES (@KlantID, @AbonnementID, @AanvraagDatum, @Status, @NummerBehouden, @NieuwNummer, @DigitaleHandtekening, @HandtekeningDatum)";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@KlantID", aanvraag.KlantId);
        command.Parameters.AddWithValue("@AbonnementID", aanvraag.AbonnementId);
        command.Parameters.AddWithValue("@AanvraagDatum", aanvraag.AanvraagDatum);
        command.Parameters.AddWithValue("@Status", aanvraag.Status);
        command.Parameters.AddWithValue("@NummerBehouden", (object?)aanvraag.NummerBehouden ?? DBNull.Value);
        command.Parameters.AddWithValue("@NieuwNummer", (object?)aanvraag.NieuwNummer ?? DBNull.Value);
        command.Parameters.AddWithValue("@DigitaleHandtekening", aanvraag.DigitaleHandtekening);
        command.Parameters.AddWithValue("@HandtekeningDatum", aanvraag.HandtekeningDatum);
        command.ExecuteNonQuery();
    }

    public List<AanvraagOverzichtDto> HaalAlleOp()
    {
        var lijst = new List<AanvraagOverzichtDto>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = @"SELECT a.ID, k.Naam, k.Email, ab.Naam AS AbonnementNaam, a.AanvraagDatum, a.Status
                    FROM Aanvraag a
                    JOIN Klant k ON a.KlantID = k.ID
                    JOIN Abonnement ab ON a.AbonnementID = ab.ID
                    ORDER BY a.AanvraagDatum DESC";
        using var command = new SqlCommand(sql, connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            lijst.Add(new AanvraagOverzichtDto
            {
                Id = (int)reader["ID"],
                KlantNaam = (string)reader["Naam"],
                KlantEmail = (string)reader["Email"],
                AbonnementNaam = (string)reader["AbonnementNaam"],
                AanvraagDatum = (DateTime)reader["AanvraagDatum"],
                Status = (string)reader["Status"]
            });
        }
        return lijst;
    }
    public AanvraagDetailDto? HaalDetailOp(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = @"SELECT a.ID, k.Naam, k.Adres, k.Geboortedatum, k.Email, k.Telefoon, k.FotoID, k.FotoBankpas,
                ab.Naam AS AbonnementNaam, a.AanvraagDatum, a.Status, a.NummerBehouden, a.DigitaleHandtekening
                FROM Aanvraag a
                JOIN Klant k ON a.KlantID = k.ID
                JOIN Abonnement ab ON a.AbonnementID = ab.ID
                WHERE a.ID = @Id";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);
        using var reader = command.ExecuteReader();
        if (!reader.Read()) return null;
        return new AanvraagDetailDto
        {
            Id = (int)reader["ID"],
            KlantNaam = (string)reader["Naam"],
            KlantAdres = (string)reader["Adres"],
            KlantGeboortedatum = (DateTime)reader["Geboortedatum"],
            KlantEmail = (string)reader["Email"],
            KlantTelefoon = (string)reader["Telefoon"],
            FotoLegitimatie = (string)reader["FotoID"],
            FotoBankpas = (string)reader["FotoBankpas"],
            AbonnementNaam = (string)reader["AbonnementNaam"],
            AanvraagDatum = (DateTime)reader["AanvraagDatum"],
            Status = (string)reader["Status"],
            NummerBehouden = reader["NummerBehouden"] == DBNull.Value ? null : (bool)reader["NummerBehouden"],
            DigitaleHandtekening = (string)reader["DigitaleHandtekening"]
        };
    }

    public void StatusBijwerken(int id, string status)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = "UPDATE Aanvraag SET Status = @Status WHERE ID = @Id";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Status", status);
        command.Parameters.AddWithValue("@Id", id);
        command.ExecuteNonQuery();
    }
}