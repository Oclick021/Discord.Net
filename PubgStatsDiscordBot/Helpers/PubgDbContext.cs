using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pubg.Net;
using PubgStatsDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PubgStatsDiscordBot.Helpers
{
    public class PubgDbContext : DbContext
    {
        //Enables these commonly used commands:
        //Add-Migration
        //Drop-Database
        //Get-DbContext
        //Scaffold-DbContext
        //Script-Migrations
        //Update-Database

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer(@"Server=.\;Database=pubg;Trusted_Connection=True;MultipleActiveResultSets=true")
                .EnableSensitiveDataLogging(true)
                .EnableDetailedErrors(true);
            ;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerMatch>()
                .HasKey(t => new { t.PlayerId, t.MatchId });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<SeasonStats> SeasonStats { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<ParticipantsStats> ParticipantsStats { get; set; }




    }
}
