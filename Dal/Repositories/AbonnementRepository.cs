using Interface.Models;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class AbonnementRepository : IAbonnementRepository
{
    private readonly string _connectionString;

    public AbonnementRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Abonnement> HaalActieveAbonnementenOp()
    {
        var abonnementen = new List<Abonnement>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var sql = @"
            SELECT ID, Naam, Provider, PrijsPerMaand, IsActief, Beschrijving
            FROM Abonnement
            WHERE IsActief = 1";

            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var abonnement = new Abonnement
                    {
                        Id = (int)reader["ID"],
                        Naam = (string)reader["Naam"],
                        Provider = (string)reader["Provider"],
                        PrijsPerMaand = (decimal)reader["PrijsPerMaand"],
                        IsActief = (bool)reader["IsActief"],
                        Beschrijving = (string)reader["Beschrijving"]
                    };

                    abonnementen.Add(abonnement);
                }
            }
        }

        return abonnementen;
    }
}
        
    
    
    
















