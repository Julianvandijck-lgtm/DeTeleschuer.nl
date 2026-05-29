using Interface.Models;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class NotitieRepository : INotitieRepository
{
    private readonly string _connectionString;
    public NotitieRepository(string connectionString) => _connectionString = connectionString;

    public List<Notitie> HaalOpVoorAanvraag(int aanvraagId)
    {
        var lijst = new List<Notitie>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = "SELECT ID, AanvraagID, Tekst, DatumAangemaakt FROM Notitie WHERE AanvraagID = @Id";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", aanvraagId);
        using var reader = command.ExecuteReader();
        while (reader.Read())
            lijst.Add(new Notitie
            {
                Id = (int)reader["ID"],
                AanvraagId = (int)reader["AanvraagID"],
                Tekst = (string)reader["Tekst"],
                DatumAangemaakt = (DateTime)reader["DatumAangemaakt"]
            });
        return lijst;
    }

    public void Opslaan(Notitie notitie)
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

    public void Bijwerken(Notitie notitie)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = "UPDATE Notitie SET Tekst = @Tekst WHERE ID = @Id";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Tekst", notitie.Tekst);
        command.Parameters.AddWithValue("@Id", notitie.Id);
        command.ExecuteNonQuery();
    }
}