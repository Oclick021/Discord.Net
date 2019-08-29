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
     

        public static Player GetPlayerByName(string name)
        {
            var playerFound = new PubgDbContext().Players.Where(p => p.Name == name).FirstOrDefault();
            if (playerFound == null)
            {
                playerFound = new Player(name: name);
            }
            return playerFound;
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
