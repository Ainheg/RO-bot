using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using RO_bot.Config;
using RO_bot.Services;
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
            using (var services = ConfigureServices()) {
                LoadConfig("botConfig.json");
                _client = services.GetRequiredService<DiscordSocketClient>();
                _client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;

                await _client.LoginAsync(TokenType.Bot, _config.BotToken);
                await _client.StartAsync();

                await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                await Task.Delay(-1);
            }
        }

        private Task LogAsync(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private void LoadConfig(string path)
        {
            _config = new JsonReader(path).Load<BotConfig>();
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();
        }
    }
}
