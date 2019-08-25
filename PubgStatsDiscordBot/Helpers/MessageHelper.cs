using Discord;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using PubgStatsDiscordBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubgStatsDiscordBot.Helpers
{
    public class MessageHelper
    {
        DiscordSocketClient _client;
        SocketMessage _message;
        public MessageHelper(DiscordSocketClient client, SocketMessage message)
        {
            _client = client;
            _message = message;
        }
        public async Task<bool> IsAuthorized()
        {

            //Checks if the server is authorized
            var chnl = _message.Channel as SocketGuildChannel;
            if (chnl == null)
            {
                if (!_client.GetChannel(Credentials.ServerID).ChannelHasUser(_message.Author))
                {
                    await _message.Channel.SendMessageAsync("Only members of Pubg stats can use this bot. please consider joining our server first");
                    Console.WriteLine($"{_message.Author.Username} is Not a member");
                    return false;
                   
                }
            }
            else
            {
                if (chnl.Guild.Id != Credentials.ServerID)
                {

                    await _message.Channel.SendMessageAsync("https://discord.gg/3FsMG3", embed: new EmbedBuilder() { Description = "این بات موقتا فقط برای استفاده در PubgStat میباشد لطفا ابتدا عوض گروه شوید", Color = Color.Gold }.Build());
                    Console.WriteLine($"{_message.Author.Username} is Not a member");
                    return false;
                }
            }
            return true;
        }
        public async Task GetStats(SocketMessage message)
        {
            string playerName = message.Content.Replace("!stats", "").Trim();
            if (playerName.Contains(" "))
            {
                await message.Channel.SendMessageAsync("!خطا : اسم حاویه فاصله است");
            }

            var player = Player.GetPlayerByName(playerName);
            if (player.Id == null)
            {
                await message.Channel.SendMessageAsync("بازیکنی با این نام یافت نشد");
            }
            else
            {
                player.GetPlayerStats();
                await message.Channel.SendMessageAsync(embed: EmbedHelper.GetStats($"{playerName}'s Solo FPP", player.SoloStats, Color.Red));
                await message.Channel.SendMessageAsync(embed: EmbedHelper.GetStats($"{playerName}'s Duo FPP", player.DuoStats, Color.Blue));
                await message.Channel.SendMessageAsync(embed: EmbedHelper.GetStats($"{playerName}'s Squad FPP", player.SquadStats, Color.Teal));

            }
        }
        public async Task GetStatsCompare(SocketMessage message)
        {
            string[] playersName = message.Content.Replace("!compare", "").Trim().Split(" ");
            if (playersName.Length < 2)
            {
                await message.Channel.SendMessageAsync("خطا: اسم 2 بازیکن را وارد کنید!");
            }

            var players = Player.GetPlayersByName(playersName);

            if (players.Count == 0)
            {
                await message.Channel.SendMessageAsync("بازیکنی با این نام ها یافت نشد");
            }
            else
            {
                foreach (var player in players)
                {
                    player.GetPlayerStats();
                }
                await message.Channel.SendMessageAsync(embed: EmbedHelper.GetCompare($"Compare", players, Color.Red));

            }
        }
    }
}
