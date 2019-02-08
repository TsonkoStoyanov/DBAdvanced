using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using VillainNames;

namespace PrintAllMinionNames
{
    class StartUp
    {
        static void Main()
        {
            using (SqlConnection connection = new SqlConnection(Configuration.connection))
            {
                connection.Open();

                List<string> names = GetMinionsNames(connection);
                SpecialPrint(names);

                connection.Close();
            }
        }

        private static void SpecialPrint(List<string> names)
        {
            for (int first = 0, last = names.Count - 1; first <= last; first++, last--)
            {
                Console.WriteLine(names[first]);
                if (first != last)
                {
                    Console.WriteLine(names[last]);
                }
            }
        }

        private static List<string> GetMinionsNames(SqlConnection connection)
        {
            List<string> names = new List<string>();

            string minionsNames = "SELECT Name FROM Minions";

            using (SqlCommand command = new SqlCommand(minionsNames,connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names.Add((string)reader[0]);
                    }
                }
            }
            return names;
        }
    }
}
