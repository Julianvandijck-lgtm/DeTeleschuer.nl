using Interface.Dtos;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class AanvraagRepository : IAanvraagRepository
{
    private readonly string _connectionString;

    public AanvraagRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Opslaan(AanvraagDto aanvraag)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = @"INSERT INTO Aanvraag (KlantID, AbonnementID, AanvraagDatum, Status, NummerBehouden, DigitaleHandtekening, HandtekeningDatum)
                        VALUES (@KlantID, @AbonnementID, @AanvraagDatum, @Status, @NummerBehouden, @DigitaleHandtekening, @HandtekeningDatum)";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@KlantID", aanvraag.KlantId);
            command.Parameters.AddWithValue("@AbonnementID", aanvraag.AbonnementId);
            command.Parameters.AddWithValue("@AanvraagDatum", aanvraag.AanvraagDatum);
            command.Parameters.AddWithValue("@Status", aanvraag.Status);
            command.Parameters.AddWithValue("@NummerBehouden", (object?)aanvraag.NummerBehouden ?? DBNull.Value);
            command.Parameters.AddWithValue("@DigitaleHandtekening", aanvraag.DigitaleHandtekening);
            command.Parameters.AddWithValue("@HandtekeningDatum", aanvraag.HandtekeningDatum);
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het opslaan van de aanvraag.", ex);
        }
    }

    public List<AanvraagOverzichtDto> HaalAlleOp()
    {
        try
        {
            var lijst = new List<AanvraagOverzichtDto>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = @"SELECT a.ID, k.Naam, k.Email, ab.Naam AS AbonnementNaam, ab.Provider, a.AanvraagDatum, a.Status
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
                    Provider = (string)reader["Provider"],
                    AanvraagDatum = (DateTime)reader["AanvraagDatum"],
                    Status = (string)reader["Status"]
                });
            }

            return lijst;
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het ophalen van aanvragen.", ex);
        }
    }

    public AanvraagDetailDto? HaalDetailOp(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = @"SELECT a.ID, k.Naam, k.Straatnaam, k.Huisnummer, k.Geboortedatum, k.Email, k.Telefoon,
                               k.FotoID, k.FotoBankpas, ab.Naam AS AbonnementNaam,
                               a.AanvraagDatum, a.Status, a.NummerBehouden, a.DigitaleHandtekening
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
                KlantStraatnaam = (string)reader["Straatnaam"],
                KlantHuisnummer = (string)reader["Huisnummer"],
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
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het ophalen van de aanvraagdetails.", ex);
        }
    }

    public void StatusBijwerken(int id, string status)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = "UPDATE Aanvraag SET Status = @Status WHERE ID = @Id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Status", status);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het bijwerken van de aanvraagstatus.", ex);
        }
    }
}
