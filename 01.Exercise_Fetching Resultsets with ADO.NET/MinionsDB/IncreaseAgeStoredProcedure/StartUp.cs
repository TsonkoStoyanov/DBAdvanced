using System;
using System.Data.SqlClient;
using VillainNames;

namespace IncreaseAgeStoredProcedure
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.connection))
            {
                connection.Open();

                GetOlder(id, connection);
                PrintMinions(id, connection);
                connection.Close();
            }

        }

        private static void PrintMinions(int id, SqlConnection connection)
        {
            string minionInfo = "SELECT Name, Age FROM Minions WHERE Id = @Id";
            using (SqlCommand command = new SqlCommand(minionInfo, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{(string)reader[0]} - {(int)reader[1]} years old");
                    }
                }

            }

           
        }

        private static void GetOlder(int id, SqlConnection connection)
        {
            string getOlder = "EXEC usp_GetOlder @Id";

            using (SqlCommand command = new SqlCommand(getOlder, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }

}







//command = new SqlCommand("SELECT * FROM Minions WHERE Id = @Id", connection);
//command.Parameters.AddWithValue("@Id", id);

//var reader = command.ExecuteReader();

//using (reader)
//{
//reader.Read();

//Console.WriteLine($"{(string) reader["Name"]} - {(int) reader["Age"]} years old");
//}
