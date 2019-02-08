using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VillainNames;

namespace ChangeTownNamesCasing
{
    class StartUp
    {
        static void Main()
        {
            string countryName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(Configuration.connection))
            {
                connection.Open();

                string countryIdSql = "SELECT c.Id  FROM Towns AS t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @countryName";

                int countryId = 0;

                using (SqlCommand command = new SqlCommand(countryIdSql, connection))
                {
                    command.Parameters.AddWithValue("@countryName", countryName);

                    if (command.ExecuteScalar() == null)
                    {
                        Console.WriteLine($"No town names were affected.");
                    }
                    else
                    {
                        countryId = (int)command.ExecuteScalar();
                     
                    }

                }

                int update = UpdateNames(countryId, connection);
            
                List<string> names = GetNames(countryId, connection);
             
                
                if (update > 0)
                {
                    Console.WriteLine($"{update} town names were affected. ");
                    Console.WriteLine($"[{string.Join(", ", names)}]");
                }

                connection.Close();
            }
        }

        private static int UpdateNames(int countryId, SqlConnection connection)
        {
            string updateSql = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = @countryId";

            using (SqlCommand command = new SqlCommand(updateSql, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);
                return command.ExecuteNonQuery();
            }
        }

        private static List<string> GetNames(int countryId, SqlConnection connection)
        {
            string namesSql = "SELECT Name FROM Towns WHERE CountryCode = @countryId";
            List<string> result = new List<string>();
            using (SqlCommand command = new SqlCommand(namesSql, connection))
            {
                command.Parameters.AddWithValue("@countryId", countryId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add((string)reader[0]);
                    }
                }
            }

            return result;
        }
    }
}
