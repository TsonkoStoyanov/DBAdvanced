using System;
using P03_FootballBetting.Data;
using P03_FootballBetting.Data.Models;


namespace P03_FootballBetting
{
    public class StartUp
    {
        public static void Main()
        {
            using (FootballBettingContext context = new FootballBettingContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
