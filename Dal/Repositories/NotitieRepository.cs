using Interface.Dtos;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class NotitieRepository : INotitieRepository
{
    private readonly string _connectionString;
    public NotitieRepository(string connectionString) => _connectionString = connectionString;

    public List<NotitieDto> HaalOpVoorAanvraag(int aanvraagId)
    {
        try
        {
            var lijst = new List<NotitieDto>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT ID, AanvraagID, Tekst, DatumAangemaakt FROM Notitie WHERE AanvraagID = @Id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", aanvraagId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                lijst.Add(new NotitieDto
                {
                    Id = (int)reader["ID"],
                    AanvraagId = (int)reader["AanvraagID"],
                    Tekst = (string)reader["Tekst"],
                    DatumAangemaakt = (DateTime)reader["DatumAangemaakt"]
                });
            return lijst;
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het ophalen van notities.", ex);
        }
    }

    public void Opslaan(NotitieDto notitie)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "INSERT INTO Notitie (AanvraagID, Tekst, DatumAangemaakt) VALUES (@AanvraagId, @Tekst, @Datum)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@AanvraagId", notitie.AanvraagId);
            command.Parameters.AddWithValue("@Tekst", notitie.Tekst);
            command.Parameters.AddWithValue("@Datum", notitie.DatumAangemaakt);
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het opslaan van de notitie.", ex);
        }
    }

    public void Bijwerken(int id, string tekst)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "UPDATE Notitie SET Tekst = @Tekst WHERE ID = @Id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Tekst", tekst);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het bijwerken van de notitie.", ex);
        }
    }
}
