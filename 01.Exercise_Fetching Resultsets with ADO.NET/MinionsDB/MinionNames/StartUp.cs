using System;
using System.Data.SqlClient;
using VillainNames;

namespace MinionNames
{
    class StartUp
    {
        static void Main()
        {
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection=new SqlConnection(Configuration.connection))
            {
                connection.Open();

                string villainNameSql = "SELECT Name FROM Villains WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(villainNameSql, connection))
                {
                    command.Parameters.AddWithValue("@id", villainId);
                    string villainName = (string) command.ExecuteScalar();

                    if (villainName == null)
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                    }
                    else
                    {
                        Console.WriteLine($"Villain: {villainName}");

                        string minions =
                            "SELECT [Name], Age FROM Minions AS m JOIN MinionsVillains AS mv ON mv.MinionId = m.Id WHERE mv.VillainId = @id";

                        using (SqlCommand command1 = new SqlCommand(minions, connection))
                        {

                            command1.Parameters.AddWithValue("@id", villainId);
                            using (SqlDataReader reader = command1.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    Console.WriteLine($"(no minions)");
                                }
                                else
                                {
                                    int index = 1;
                                    while (reader.Read())
                                    {
                                        Console.WriteLine($"{index++}. {reader[0]} {reader[1]}");
                                    }
                                }
                            }  
                        }
                    }
                }

               connection.Close();

            }
        }
    }
}
