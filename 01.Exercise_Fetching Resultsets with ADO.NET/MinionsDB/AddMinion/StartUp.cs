using System;
using System.Data.SqlClient;
using VillainNames;

namespace AddMinion
{
    class StartUp
    {
        static void Main()
        {

            string[] minionInput = Console.ReadLine().Split();
            string[] villainInput = Console.ReadLine().Split();
            
            string minionName = minionInput[1];
            int age = int.Parse(minionInput[2]);
            string town = minionInput[3];

            string villainName = villainInput[1];
            
            using (SqlConnection connection = new SqlConnection(Configuration.connection))
            {
                connection.Open();

                int townId = GetTownId(town, connection);
                int villainId = GetVillainId(villainName, connection);
                int minionId = InsertMinionGetId(minionName, age, townId, connection);
                AssignMinionVillain(villainId, minionId, connection);
                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");


                connection.Close();
            }
        }

        private static void AssignMinionVillain(int villainId, int minionId, SqlConnection connection)
        {
            string insert = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                command.Parameters.AddWithValue("@minionId", minionId);
                command.Parameters.AddWithValue("@villainId", villainId);
                command.ExecuteNonQuery();
                
            }
        }

        private static int InsertMinionGetId(string minionName, int age, int townId, SqlConnection connection)
        {
            string insert = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townid)";

            using (SqlCommand command= new SqlCommand(insert, connection))
            {
                command.Parameters.AddWithValue("@name", minionName);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@townid", townId);

                command.ExecuteNonQuery();
            }

            string minionsSql = "SELECT Id FROM Minions WHERE Name = @name";

            using (SqlCommand command = new SqlCommand(minionsSql, connection))
            {
                command.Parameters.AddWithValue("@name", minionName);

                return (int)command.ExecuteScalar();
            }

        }

        private static int GetVillainId(string villainName, SqlConnection connection)
        {
            string villainSql = "SELECT id FROM Villains WHERE [Name] = @Name";

            using (SqlCommand command = new SqlCommand(villainSql, connection))
            {
                command.Parameters.AddWithValue("@Name", villainName);

                if (command.ExecuteScalar() == null)
                {
                    InsertVillain(villainName, connection);
                }

                return (int)command.ExecuteScalar();
            }
        }

        private static void InsertVillain(string villainName, SqlConnection connection)
        {
            string insert = "INSERT INTO Villains (Name, EvilnessFactorId) VALUES (@name, 4)";

            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                command.Parameters.AddWithValue("@name", villainName);
                command.ExecuteNonQuery();
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
        }

        private static int GetTownId(string town, SqlConnection connection)
        {
            string townId = "SELECT id FROM Towns WHERE [Name] = @town";

            using (SqlCommand command = new SqlCommand(townId, connection))
            {
                command.Parameters.AddWithValue("@town", town);

                if (command.ExecuteScalar() == null)
                {
                    InsertTown(town, connection);
                }

                return (int)command.ExecuteScalar();
            }


        }

        private static void InsertTown(string town, SqlConnection connection)
        {
            string insert = "INSERT INTO Towns (Name) VALUES (@town)";

            using (SqlCommand command = new SqlCommand(insert, connection))
            {
                command.Parameters.AddWithValue("@town", town);
                command.ExecuteNonQuery();
                Console.WriteLine($"Town {town} was added to the database.");
            }
        }
    }
}
