using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using VillainNames;

namespace IncreaseMinionAge
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputId = Console.ReadLine().Split();

            using (SqlConnection connection = new SqlConnection(Configuration.connection))
            {
                connection.Open();

                UpdateMinions(inputId, connection);
                PrintMinions(inputId, connection);

                connection.Close();
            }
        }

        private static void PrintMinions(string[] inputId, SqlConnection connection)
        {
            string minions = "SELECT Name, Age FROM Minions WHERE Id IN (@ids)".Replace("@ids", string.Join(", ", inputId));

            using (SqlCommand command = new SqlCommand(minions, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]} {reader[1]}");
                    }
                }
            }
        }

        private static void UpdateMinions(string[] inputId, SqlConnection connection)
        {
            string minionsToUpdate = "UPDATE Minions SET Name = UPPER(LEFT(Name, 1)) + LOWER(RIGHT(Name, LEN(Name)-1)),Age += 1 WHERE Id IN(@ids)".Replace("@ids", string.Join(", ", inputId));

            using (SqlCommand command = new SqlCommand(minionsToUpdate, connection))
            {

                command.ExecuteNonQuery();
            }

          
        }
    }
}
