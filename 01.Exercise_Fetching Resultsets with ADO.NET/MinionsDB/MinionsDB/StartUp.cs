using System;
using System.Data.SqlClient;

namespace InitialSetup
{
    public class StartUp
    {
        public static void Main()
        {
            using (SqlConnection conection = new SqlConnection(Configuration.connectionString))
            {
                conection.Open();

                string database = "CREATE DATABASE MinionsDB";

                ExecuteNonQuery(conection, database);

                conection.ChangeDatabase("MinionsDB");

                string createTableCountries = "CREATE TABLE Countries (Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50))";

                string createTableTowns =
                    "CREATE TABLE Towns(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))";

                string createTableMinions =
                    "CREATE TABLE Minions(Id INT PRIMARY KEY IDENTITY,Name VARCHAR(30), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))";

                string createTableEvilnesFactor = "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50))";

                string createTableVilians =
                    "CREATE TABLE Villains (Id INT PRIMARY KEY IDENTITY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))";

                string createTableMinionsVilians =
                    "CREATE TABLE MinionsVillains (MinionId INT FOREIGN KEY REFERENCES Minions(Id),VillainId INT FOREIGN KEY REFERENCES Villains(Id),CONSTRAINT PK_MinionsVillains PRIMARY KEY (MinionId, VillainId))";

                ExecuteNonQuery(conection, createTableCountries);
                ExecuteNonQuery(conection,createTableTowns);
                ExecuteNonQuery(conection,createTableMinions);
                ExecuteNonQuery(conection, createTableEvilnesFactor);
                ExecuteNonQuery(conection,createTableVilians);
                ExecuteNonQuery(conection, createTableMinionsVilians);

                string insertCountries = "INSERT INTO Countries ([Name]) VALUES ('Bulgaria'),('England'),('Cyprus'),('Germany'),('Norway')";

                string insertTowns =
                    "INSERT INTO Towns ([Name], CountryCode) VALUES ('Plovdiv', 1),('Varna', 1),('Burgas', 1),('Sofia', 1),('London', 2),('Southampton', 2),('Bath', 2),('Liverpool', 2),('Berlin', 3),('Frankfurt', 3),('Oslo', 4)";

                string insertMinions = "INSERT INTO Minions (Name,Age, TownId) VALUES('Bob', 42, 3),('Kevin', 1, 1),('Bob ', 32, 6),('Simon', 45, 3),('Cathleen', 11, 2),('Carry ', 50, 10),('Becky', 125, 5),('Mars', 21, 1),('Misho', 5, 10),('Zoe', 125, 5),('Json', 21, 1)";

                string insertEvilnesFactor =
                    "INSERT INTO EvilnessFactors (Name) VALUES ('Super good'),('Good'),('Bad'), ('Evil'),('Super evil')";

                string insertVilians =
                    "INSERT INTO Villains (Name, EvilnessFactorId) VALUES ('Gru',2),('Victor',1),('Jilly',3),('Miro',4),('Rosen',5),('Dimityr',1),('Dobromir',2)";

                string insertMinionsVilians =
                    "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (4,2),(1,1),(5,7),(3,5),(2,6),(11,5),(8,4),(9,7),(7,1),(1,3),(7,3),(5,3),(4,3),(1,2),(2,1),(2,7)";

                ExecuteNonQuery(conection, insertCountries);
                ExecuteNonQuery(conection, insertTowns);
                ExecuteNonQuery(conection, insertMinions);
                ExecuteNonQuery(conection, insertEvilnesFactor);
                ExecuteNonQuery(conection, insertVilians);
                ExecuteNonQuery(conection, insertMinionsVilians);
                conection.Close();
            }
        }

        private static void ExecuteNonQuery(SqlConnection conection, string database)
        {
            using (SqlCommand command = new SqlCommand(database, conection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}