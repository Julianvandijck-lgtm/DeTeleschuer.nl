using Interface.Models;
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

    public int Opslaan(Klant klant)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var sql = @"
                INSERT INTO Klant (Naam, Adres, Geboortedatum, Email, Telefoon, FotoID, FotoBankpas)
                VALUES (@Naam, @Adres, @Geboortedatum, @Email, @Telefoon, @FotoID, @FotoBankpas);
                SELECT SCOPE_IDENTITY();";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Naam", klant.Naam);
                command.Parameters.AddWithValue("@Adres", klant.Adres);
                command.Parameters.AddWithValue("@Geboortedatum", klant.GeboorteDatum);
                command.Parameters.AddWithValue("@Email", klant.Email);
                command.Parameters.AddWithValue("@Telefoon", klant.Telefoon);
                command.Parameters.AddWithValue("@FotoID", klant.FotoID);
                command.Parameters.AddWithValue("@FotoBankpas", klant.FotoBankpas);

                var id = command.ExecuteScalar();
                return Convert.ToInt32(id);
            }
        }
    }
    public int? HaalIdOpViaEmail(string email)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var sql = "SELECT ID FROM Klant WHERE Email = @Email";
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Email", email);
        var result = command.ExecuteScalar();
        return result == null ? null : (int)result;
    }
}