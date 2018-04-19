using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.modules
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        
        private CommandService _service;

        public Help(CommandService service)
        {
            _service = service;
        }

        [Command("help"), Summary("Shows all the commands.....")]
        public async Task HelpAsync()
        {
            var builder = new EmbedBuilder()
            {
                Color = Color.DarkBlue,
            };

            foreach (var module in _service.Modules)
            {
                int i = 0;
                string description = null;
                foreach (var cmd in module.Commands)
                {
                    i++;
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{i}) !{cmd.Aliases.First()} | {cmd.Summary}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}
