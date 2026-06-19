using Interface.Dtos;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class GebruikerRepository : IGebruikerRepository
{
    private readonly string _connectionString;

    public GebruikerRepository(string connectionString) => _connectionString = connectionString;

    public GebruikerDto? HaalOpViaGebruikersnaam(string gebruikersnaam)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = "SELECT ID, Gebruikersnaam, WachtwoordHash FROM Gebruiker WHERE Gebruikersnaam = @Naam";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Naam", gebruikersnaam);
            using var reader = command.ExecuteReader();

            if (!reader.Read()) return null;

            return new GebruikerDto
            {
                Id = (int)reader["ID"],
                Gebruikersnaam = (string)reader["Gebruikersnaam"],
                WachtwoordHash = (string)reader["WachtwoordHash"]
            };
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het ophalen van de gebruiker.", ex);
        }
    }

    public void Aanmaken(GebruikerDto gebruiker)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = "INSERT INTO Gebruiker (Gebruikersnaam, WachtwoordHash) VALUES (@Naam, @Hash)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Naam", gebruiker.Gebruikersnaam);
            command.Parameters.AddWithValue("@Hash", gebruiker.WachtwoordHash);
            command.ExecuteNonQuery();
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het aanmaken van de gebruiker.", ex);
        }
    }
}
