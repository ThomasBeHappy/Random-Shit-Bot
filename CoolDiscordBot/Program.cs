using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using CoolDiscordBot.Misc;
using CoolDiscordBot.Misc.Guilds;
using CoolDiscordBot.services;
using CoolDiscordBot.Properties;

namespace CoolDiscordBot
{
    public class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;
        private weatherservice _weatherservice;

        private static void Main(string[] args) => new Program().StartAsync().GetAwaiter().GetResult();

        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            _weatherservice = new weatherservice();

            // Avoid hard coding your token. Use an external source instead in your code.

            

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .AddSingleton(_weatherservice)
                .BuildServiceProvider();

            await InstallCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommandAsync;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            if (Settings.Default.InLockdown == false)
            {
                // Don't process the command if it was a System Message
                var message = messageParam as SocketUserMessage;

                var guildchannel = messageParam.Channel as SocketGuildChannel;
                var config = Guilds.getorcreateguild(guildchannel.Guild);

                if (message == null) return;
                // Create a number to track where the prefix ends and the command begins
                int argPos = 0;
                // Determine if the message is a command, based on if it starts with '!' or a mention prefix
                if (!(message.HasStringPrefix(config.prefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))) return;
                // Create a Command Context
                var context = new SocketCommandContext(_client, message);
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully)
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                    await context.Channel.SendMessageAsync(result.ErrorReason);
            }
        }
    }
}
