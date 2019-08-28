using System;
using System.Collections.Generic;
using System.Text;

namespace PubgStatsDiscordBot.Helpers
{
    public class Credentials
    {
        private static string pubgToken;
        private static string discordToken;
        public static string PubgToken
        {
            get
            {
                if (pubgToken == null)
                {
                    pubgToken = Environment.GetEnvironmentVariable("pubgtoken", EnvironmentVariableTarget.User);
                }
                return pubgToken;
            }
            set => pubgToken = value;
        }
        public static string DiscordToken
        {
            get
            {
                if (discordToken == null)
                {
                    discordToken = Environment.GetEnvironmentVariable("discordtoken", EnvironmentVariableTarget.User);
                }
                return discordToken;
            }
            set => discordToken = value;
        }

        public static ulong UserID = 0;
        public static ulong ChannelID = 614400058364002314;
        public static ulong ServerID = 270639957834465281;
 


    }
}
