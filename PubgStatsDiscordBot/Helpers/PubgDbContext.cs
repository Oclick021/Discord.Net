using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PubgStatsDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PubgStatsDiscordBot.Helpers
{
   public class PubgDbContext: DbContext
    {
        //Enables these commonly used commands:
        //Add-Migration
        //Drop-Database
        //Get-DbContext
        //Scaffold-DbContext
        //Script-Migrations
        //Update-Database
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        //public DbSet<Player> Players { get; set; }
        public DbSet<SeasonStats> Stats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=pubg.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }


      
    }
}
