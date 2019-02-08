
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data.Models;

namespace P03_FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {

        public FootballBettingContext()
        {
        }

        public FootballBettingContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Bet> Bets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Country> Countries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.connectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerStatistic>().HasKey(x => new { x.PlayerId, x.GameId});

            //color

            builder.Entity<Color>()
                .HasMany(pt => pt.PrimaryKitTeams)
                .WithOne(pc => pc.PrimaryKitColor)
                .HasForeignKey(pc => pc.PrimaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Color>()
                .HasMany(st => st.SecondaryKitTeams)
                .WithOne(sc => sc.SecondaryKitColor)
                .HasForeignKey(sc => sc.SecondaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            //Team

            builder.Entity<Team>()
                .HasMany(h => h.HomeGames)
                .WithOne(t => t.HomeTeam)
                .HasForeignKey(t => t.HomeTeamId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasMany(a => a.AwayGames)
                .WithOne(t => t.AwayTeam)
                .HasForeignKey(t => t.AwayTeamId).OnDelete(DeleteBehavior.Restrict);

            //country

            builder.Entity<Country>()
                .HasMany(t => t.Towns)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId);
        }
    }
}