using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CoolDiscordBot.modules
{
    public class nsfw : ModuleBase<SocketCommandContext>
    {
        [Command("rule34")]
        [Summary("You know what it is.... Can only be run in NSFW channels")]
        public async Task StartGameAsync(params string[] tags)
        {
            if (Context.Channel.IsNsfw)
            {
                Random random = new Random();
                XmlDocument doc1 = new XmlDocument();
                doc1.Load($"https://rule34.xxx/index.php?page=dapi&s=post&q=index&limit=1&tags={String.Join(" ", tags)}&pid={random.Next(1, 20)}");
                XmlElement root = doc1.DocumentElement;
                XmlNodeList nodes = root.SelectNodes("/posts/post");
                foreach (XmlNode node in nodes)
                {
                    EmbedBuilder builder = new EmbedBuilder();
                    string url = node.Attributes["file_url"].InnerText;
                    builder.Title = Context.User.Username + "#" + Context.User.Discriminator + " | " + string.Join(" ", tags);
                    builder.Url = url;
                    builder.ImageUrl = url;
                    builder.Color = Color.DarkGreen;
                    await ReplyAsync("", false, builder.Build());
                }
            }
            else
            {
                await ReplyAsync("This is not a nsfw channel! I am not showing it here!");
            }
        }
    }
}
