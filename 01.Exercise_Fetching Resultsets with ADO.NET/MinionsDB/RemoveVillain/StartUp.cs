using System;
using System.Data.SqlClient;
using VillainNames;

namespace RemoveVillain
{
    class StartUp
    {
        static void Main( )
        {

            int inputVillainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.connection))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                int villainId = GetVillainId(inputVillainId, connection, transaction);

                if (villainId == 0)
                {
                    Console.WriteLine($"No such villain was found.");
                }
                else
                {
                    try
                    {
                        int released = ReleaseMinions(villainId, connection, transaction);
                        string villainName = GetVillainName(villainId, connection, transaction);
                        DeleteVillan(villainId, connection, transaction);

                        Console.WriteLine($"{villainName} was deleted.");
                        Console.WriteLine($"{released} minions were released.");
                    }
                    catch (SqlException e)
                    {
                       transaction.Rollback();
                        
                    }
             
                }
                
                connection.Close();
            }
        }

        private static void DeleteVillan(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string villainInfo = "DELETE FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(villainInfo, connection, transaction))
            {
                command.Parameters.AddWithValue("@id", villainId);
                command.ExecuteNonQuery();
            }

        }

        private static string GetVillainName(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string villainInfo = "SELECT Name FROM Villains WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(villainInfo, connection, transaction))
            {
                command.Parameters.AddWithValue("@id", villainId);

                return (string) command.ExecuteScalar();
            }
        }

        private static int ReleaseMinions(int villainId, SqlConnection connection, SqlTransaction transaction)
        {
            string deletedMinions = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";

            using (SqlCommand command = new SqlCommand(deletedMinions, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", villainId);
                return command.ExecuteNonQuery();
            }
        }


        private static int GetVillainId(int inputVillainId, SqlConnection connection, SqlTransaction transaction)
        {
            string villainSql = "SELECT Id FROM Villains WHERE Id = @villainId";

            using (SqlCommand command = new SqlCommand(villainSql, connection, transaction))
            {
                command.Parameters.AddWithValue("@villainId", inputVillainId);

                if (command.ExecuteScalar() == null)
                {
                    return 0;
                }

                return (int)command.ExecuteScalar();
            }
        }
    }
}
