using Discord;
using Discord.WebSocket;
using RO_bot.Config;
using System;
using System.Threading.Tasks;

namespace RO_bot
{
    class RO
    {
        private DiscordSocketClient _client;
        private BotConfig _config;

        static void Main(string[] args)
            => new RO().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            LoadConfig("botConfig.json");
            _client = new DiscordSocketClient();
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, _config.BotToken);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private void LoadConfig(string path)
        {
            _config = new JsonReader(path).Load<BotConfig>();
        }
    }
}
