using System;
using System.Data.SqlClient;

namespace VillainNames
{
    class StartUp
    {
        static void Main()
        {
            using (SqlConnection connection  = new SqlConnection(Configuration.connection))
            {
                connection.Open();
                string vilianInfo = "SELECT v.[Name], COUNT(mv.MinionId) AS Minion FROM Villains AS v JOIN MinionsVillains AS mv ON mv.VillainId = v.Id GROUP BY v.Name HAVING COUNT(MinionId) >= 3 ORDER BY Minion DESC";

                using (SqlCommand command = new SqlCommand(vilianInfo, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} -> {reader[1]}");                            
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}
