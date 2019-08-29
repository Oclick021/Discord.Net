using System;
using System.Collections.Generic;
using System.Text;

namespace PubgStatsDiscordBot.Models
{
  public class PlayerMatch
    {
        public string   PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public string   MatchId { get; set; }
        public virtual Match Match { get; set; }
    }
}
