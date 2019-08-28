using System;
using System.Collections.Generic;
using System.Text;

namespace PubgStatsDiscordBot.Models
{

    public class Match
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Duration { get; set; }
        public Roster[] Rosters { get; set; }
        public string GameMode { get; set; }
        public string TitleId { get; set; }
        public string MapName { get; set; }
        public bool IsCustomMatch { get; set; }
        public string SeasonState { get; set; }
        public string ShardId { get; set; }


        public Match()
        {

        }
     
    }



   

}
