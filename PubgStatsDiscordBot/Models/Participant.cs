﻿namespace PubgStatsDiscordBot.Models
{
    public class Participant
    {
        public string Id { get; set; }
        public ParticipantsStats Stats { get; set; }
        public string Actor { get; set; }
        public string ShardId { get; set; }
    }

   

}
