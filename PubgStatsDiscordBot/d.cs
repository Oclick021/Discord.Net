using System;
using System.Collections.Generic;
using System.Text;

namespace PubgStatsDiscordBot
{

    public class Rootobject
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Duration { get; set; }
        public Roster[] Rosters { get; set; }
        public object Rounds { get; set; }
        public Asset[] Assets { get; set; }
        public object Stats { get; set; }
        public string GameMode { get; set; }
        public object PatchVersion { get; set; }
        public string TitleId { get; set; }
        public Links Links { get; set; }
        public string MapName { get; set; }
        public bool IsCustomMatch { get; set; }
        public string SeasonState { get; set; }
        public string ShardId { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string schema { get; set; }
    }

    public class Roster
    {
        public string Id { get; set; }
        public Stats Stats { get; set; }
        public bool Won { get; set; }
        public Participant[] Participants { get; set; }
        public string ShardId { get; set; }
    }

    public class Stats
    {
        public int Rank { get; set; }
        public int TeamId { get; set; }
    }

    public class Participant
    {
        public string Id { get; set; }
        public Stats1 Stats { get; set; }
        public string Actor { get; set; }
        public string ShardId { get; set; }
    }

    public class Stats1
    {
        public string Name { get; set; }
        public string PlayerId { get; set; }
        public int DBNOs { get; set; }
        public int Assists { get; set; }
        public int Boosts { get; set; }
        public float DamageDealt { get; set; }
        public string DeathType { get; set; }
        public int HeadshotKills { get; set; }
        public int Heals { get; set; }
        public int KillPlace { get; set; }
        public int KillPointsDelta { get; set; }
        public int KillStreaks { get; set; }
        public int Kills { get; set; }
        public int LastKillPoints { get; set; }
        public int LastWinPoints { get; set; }
        public float LongestKill { get; set; }
        public int MostDamage { get; set; }
        public int Revives { get; set; }
        public float RideDistance { get; set; }
        public int RoadKills { get; set; }
        public float SwimDistance { get; set; }
        public int TeamKills { get; set; }
        public float TimeSurvived { get; set; }
        public int VehicleDestroys { get; set; }
        public float WalkDistance { get; set; }
        public int WeaponsAcquired { get; set; }
        public int WinPlace { get; set; }
        public int WinPointsDelta { get; set; }
    }

    public class Asset
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }

}
