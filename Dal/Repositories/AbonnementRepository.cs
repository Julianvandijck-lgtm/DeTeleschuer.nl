using Interface.Models;
using Interface.Repositories;
using Microsoft.Data.SqlClient;

namespace Dal.Repositories;

public class AbonnementRepository : IAbonnementRepository // De methode van de interface moet hier aanwezig zijn 
{
    private readonly string _connectionString;

    public AbonnementRepository(string connectionString) // constructor word aangemaakt met adres van database
    {
        _connectionString = connectionString; // parameter word opgeslagen als field zodat ik hem later kan meenemen
    }

    public List<Abonnement> HaalActieveAbonnementenOp() // ik gebruik de methode die ik in Iabo heb aangemaakt
    {
        var abonnementen = new List<Abonnement>(); // ik maak een lijst aan zodat de data daadwerkelijk in een lijst gezet kan worden
// met using word de code auto afgesloten 
        using (var connection = new SqlConnection(_connectionString)) // Ik maak hier een verbindings object aan met mijn database adress
        { // eigenlijk gewoon zorgen dat ie geconnect staat aan de data 
            connection.Open();

            var sql = @"
            SELECT ID, Naam, Provider, PrijsPerMaand, IsActief, Beschrijving 
            FROM Abonnement    
            WHERE IsActief = 1"; 
   // ik haal de kolommen op uit mn abonnent model sql gebruikt 1 of 0 dus alle actieve abo's staan op 1 dus die worden meegenomen 0 niet
            using (var command = new SqlCommand(sql, connection)) // versturen naar de juiste verbinding 
            using (var reader = command.ExecuteReader()) //rijen lezen 
            {
                while (reader.Read()) // zolang er rijen zijn blijf doorgaan (true) zodra er geen rij meer is (False) stop 
                {
                    var abonnement = new Abonnement
                    {
                        Id = (int)reader["ID"],
                        Naam = (string)reader["Naam"], // deze reader haalt ruwe data op zodat ik ze kan casten naar C# termen int etc.
                        Provider = (string)reader["Provider"],
                        PrijsPerMaand = (decimal)reader["PrijsPerMaand"],
                        IsActief = (bool)reader["IsActief"], // kolomnamen moeten overeen komen met SQL QEry R. 24
                        Beschrijving = (string)reader["Beschrijving"]
                    };

                    abonnementen.Add(abonnement); // bewust ervoor gekozen om binnen de while loop te houden zodat ie dit blijft uitvoeren totdat alle abo's verwerkt zijn
                }
            }
        }

        return abonnementen; // Ik geef hem door aan de service laag 
    }
}
        
    
    
    
















