using System;
using System.Collections.Generic;
using System.Text;

namespace PubgStatsDiscordBot.Helpers
{
    public class Credentials
    {
        public static string PubgToken { get; set; }
        public static string DiscordToken { get; set; }

        public static ulong UserID = 0;
        public static ulong ChannelID = 614400058364002314;
        public static ulong ServerID = 270639957834465281;
    }
}
