using Microsoft.EntityFrameworkCore;
using Pubg.Net;
using PubgStatsDiscordBot.Extentions;
using PubgStatsDiscordBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubgStatsDiscordBot.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SeasonStats SoloStats { get; set; }
        public SeasonStats DuoStats { get; set; }
        public SeasonStats SquadStats { get; set; }
        public DateTime? CurrentSeasionLastUpdate { get; set; }

        PubgPlayerService playerService = new PubgPlayerService();

        public Player()
        {

        }
        public Player(string id = null, string name = null)
        {
            if (id != null)
            {
                GetByID(id);
            }
            else if (name != null)
            {
                GetByName(name);
            }

        }
        public void GetByName(string playerName)
        {
            try
            {
                var playerFound = playerService.GetPlayers(PubgPlatform.Steam, new GetPubgPlayersRequest
                {
                    ApiKey = Credentials.PubgToken,
                    PlayerNames = new string[] { playerName }
                }).FirstOrDefault();
                Id = playerFound.Id;
                Name = playerName;
                GetPlayerStats();


            }
            catch (Exception)
            {

            }

        }
        public void GetByID(string id)
        {
            var playerFound = playerService.GetPlayer(PubgPlatform.Steam, id, Credentials.PubgToken);
            Id = playerFound.Id;
            Name = playerFound.Name;
            GetPlayerStats();
        }
        public void GetPlayerStats()
        {
            if (CurrentSeasionLastUpdate == null || CurrentSeasionLastUpdate.Value <= DateTime.Now.AddMinutes(-10))
            {
                var playerSeason = playerService.GetPlayerSeason(PubgPlatform.Steam, Id, SeasonStats.CurrentSeasonID, Credentials.PubgToken);
                SoloStats = new SeasonStats();
                SoloStats.Clone(playerSeason.GameModeStats.SoloFPP);
                DuoStats = new SeasonStats();
                DuoStats.Clone(playerSeason.GameModeStats.DuoFPP);
                SquadStats = new SeasonStats();
                SquadStats.Clone(playerSeason.GameModeStats.SquadFPP);
                CurrentSeasionLastUpdate = DateTime.Now;
                Save();
            }
        }


        public void Save()
        {
            using (var con = new PubgDbContext())
            {
                var player = con.Players.Find(Id);
                if (player == null)
                {
                    con.Players.Add(this);
                    con.SaveChanges();
                }
                else
                {
                    player.SoloStats = SoloStats;
                    player.DuoStats = DuoStats;
                    player.SquadStats = SquadStats;
                    player.CurrentSeasionLastUpdate = CurrentSeasionLastUpdate;
                    con.Players.Update(player);
                    con.SaveChanges();
                }
            }
        }
        public static Player GetPlayerByName(string name)
        {
            Player player;
            using (var con = new PubgDbContext())
            {
                player = con.Players
                                .Include(s => s.SoloStats)
                                .Include(d => d.DuoStats)
                                .Include(s => s.SquadStats)
                                .Where(p => p.Name == name)
                                .FirstOrDefault();
            }
            if (player == null)
            {
                player = new Player(name: name);
            }
            return player;
        }
        public static List<Player> GetPlayersByName(string[] names)
        {
            var players = new List<Player>();
            using (var con = new PubgDbContext())
            {
                foreach (var name in names)
                {
                    Player player;

                    player = con.Players
                                    .Include(s => s.SoloStats)
                                    .Include(d => d.DuoStats)
                                    .Include(s => s.SquadStats)
                                    .Where(p => p.Name == name)
                                    .FirstOrDefault();

                    if (player == null)
                    {
                        player = new Player(name: name);
                    }
                    if (player.Id != null)
                    {
                        players.Add(player);
                    }
                }
            }
            return players;
        }
    }
}
