using Newtonsoft.Json;
using Pubg.Net;
using PubgStatsDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubgStatsDiscordBot.Helpers
{
    public class PubgHelper
    {
        public static PubgPlayerSeason GetPlayerStatsByName(string playerName)
        {
            return null;
        }


        public static Match GetPubgMatch(string matchID)
        {
            var service = new PubgMatchService();
            var match = service.GetMatch(matchID, Credentials.PubgToken);

            string serialized = JsonConvert.SerializeObject(match);
            var m = JsonConvert.DeserializeObject<Match>(serialized);
            return m;
        }
    }
}
