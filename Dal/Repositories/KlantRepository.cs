using Interface.Dtos;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class KlantRepository : IKlantRepository
{
    private readonly string _connectionString;

    public KlantRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int Opslaan(KlantDto klant)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = @"INSERT INTO Klant (Naam, Straatnaam, Huisnummer, Geboortedatum, Email, Telefoon, FotoID, FotoBankpas)
                        VALUES (@Naam, @Straatnaam, @Huisnummer, @Geboortedatum, @Email, @Telefoon, @FotoID, @FotoBankpas);
                        SELECT SCOPE_IDENTITY();";

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Naam", klant.Naam);
            command.Parameters.AddWithValue("@Straatnaam", klant.Straatnaam);
            command.Parameters.AddWithValue("@Huisnummer", klant.Huisnummer);
            command.Parameters.AddWithValue("@Geboortedatum", klant.GeboorteDatum.ToDateTime(TimeOnly.MinValue));
            command.Parameters.AddWithValue("@Email", klant.Email);
            command.Parameters.AddWithValue("@Telefoon", klant.Telefoon);
            command.Parameters.AddWithValue("@FotoID", klant.FotoID);
            command.Parameters.AddWithValue("@FotoBankpas", klant.FotoBankpas);

            return Convert.ToInt32(command.ExecuteScalar());
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het opslaan van de klant.", ex);
        }
    }

    public int? HaalIdOpViaEmail(string email)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = "SELECT ID FROM Klant WHERE Email = @Email";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Email", email);

            var result = command.ExecuteScalar();
            return result == null ? null : Convert.ToInt32(result);
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het opzoeken van de klant op e-mail.", ex);
        }
    }
}
