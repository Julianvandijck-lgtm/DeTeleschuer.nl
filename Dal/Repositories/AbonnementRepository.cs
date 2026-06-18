using Interface.Dtos;
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

    public List<AbonnementDto> HaalAlleOp()
    {
        var abonnementen = new List<AbonnementDto>();

        try
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var sql = "SELECT ID, Naam, Provider, PrijsPerMaand, IsActief, Beschrijving FROM Abonnement";

            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                abonnementen.Add(new AbonnementDto
                {
                    Id = (int)reader["ID"],
                    Naam = (string)reader["Naam"],
                    Provider = (string)reader["Provider"],
                    PrijsPerMaand = (decimal)reader["PrijsPerMaand"],
                    IsActief = (bool)reader["IsActief"],
                    Beschrijving = (string)reader["Beschrijving"]
                });
            }
        }
        catch (SqlException ex)
        {
            throw new InvalidOperationException("Fout bij het ophalen van abonnementen uit de database.", ex);
        }

        return abonnementen;
    }
}
        
    
    
    
















