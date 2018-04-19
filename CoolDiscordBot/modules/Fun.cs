using Discord;
using Discord.Commands;
using NYoutubeDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeSearch;

namespace CoolDiscordBot.modules
{
    public class Fun : ModuleBase<SocketCommandContext>
    {
        public audiomodule audioModule;

        [Command("play", RunMode = RunMode.Async)]
        [Summary("Search and play music!")]
        public async Task PlayLocal(params string[] search)
        {

            int querypages = 1;

            var items = new VideoSearch();
            var message = await Context.Channel.SendMessageAsync("Searching...");
            var urls = items.SearchQuery(string.Join(" ", search), querypages);
            var duration = urls.First().Duration;
            var thumbnail = urls.First().Thumbnail;

            await message.ModifyAsync(msg => msg.Content = "Downloading " + urls.First().Title + "...");
            var urlToDownload = urls.First().Url;
            var newFilename = Guid.NewGuid().ToString();
            var mp3OutputFolder = Environment.CurrentDirectory + "/songs/";
            var downloader = new YoutubeDL();
            downloader.VideoUrl = urlToDownload;
            downloader.YoutubeDlPath = @"C:\Users\thoma\source\repos\CoolDiscordBot\CoolDiscordBot\bin\Debug\youtube-dl.exe";
            downloader.Options.FilesystemOptions.Output = Environment.CurrentDirectory + "/songs/" + newFilename;
            downloader.PrepareDownload();
            downloader.Download();
            var info = downloader.GetDownloadInfo();
            while (downloader.ProcessRunning == true)
            {
                await Task.Delay(50);
            }

            EmbedBuilder builder = new EmbedBuilder();
            builder.Title = Context.User.Username + "#" + Context.User.Discriminator;
            builder.AddField("Name", info.Title);
            builder.ThumbnailUrl = thumbnail;
            builder.AddField("Duration", duration);
            builder.AddField("Url", urlToDownload);


            await ReplyAsync("t", false, builder.Build());
            audioModule = new audiomodule();
            var voiceChannel = ((IVoiceState)Context.User).VoiceChannel;
            if (voiceChannel is null)
            {
                await ReplyAsync($"{Context.User.Mention} you are not in a voice channel!");
                return;
            }
            var audioClient = await voiceChannel.ConnectAsync().ConfigureAwait(false);
            Console.WriteLine(newFilename);
            string path = "\"" + Environment.CurrentDirectory + "/songs/" + newFilename + ".mkv" + "\"";

            await audioModule.PlayLocalMusic(path, audioClient);
        }

        [Command("Stop")]
        [Summary("Sorry cant stop yet i am working on it!")]
        public async Task stopasync()
        {

            await ReplyAsync("I aint stopping for you shitty person");
        }

        [Command("duck", RunMode = RunMode.Async)]
        [Summary("A strange mysterious command?")]
        public async Task Duckasync()
        {
            audioModule = new audiomodule();
            var voiceChannel = ((IVoiceState)Context.User).VoiceChannel;
            if (voiceChannel is null)
            {
                await ReplyAsync($"{Context.User.Mention} you are not in a voice channel!");
                return;
            }
            var audioClient = await voiceChannel.ConnectAsync().ConfigureAwait(false);
            string path = "\"" + Environment.CurrentDirectory + "/sounds/Duck (2).mp3" + "\"";

            await audioModule.PlayLocalMusic(path, audioClient);
        }

        [Command("Chat")]
        [Summary("Dont use yet its not working yet!")]
        [RequireOwner]
        public async Task StartGameAsync(params string[] args)
        {
            ulong myid = 229563674375749633;
            if (Context.User.Id == 281902494492131329 || Context.User.Id == 155392767751749632)
            {
                string message = Context.Message.Content.Substring(6);
                var me = Context.Client.GetUser(myid);

                EmbedBuilder builder = new EmbedBuilder();
                builder.Title = "Message by abyss";
                builder.Description = "***Thomas abyss messaged you HOLY FUCK!***";
                builder.AddField("Abyss", message);
                builder.Color = Color.Blue;

                await me.SendMessageAsync("", false, builder.Build());
            }
            else
            {
                string message = Context.Message.Content.Substring(6);
                ulong abyssid = 281902494492131329;
                var abyss = Context.Client.GetUser(abyssid);

                EmbedBuilder builder = new EmbedBuilder();
                builder.Title = "Message by someone";
                builder.Timestamp = DateTime.Now;
                builder.Description = "***You got a anonymous message!***";
                builder.AddField("His message", message);
                builder.Color = Color.Blue;

                await abyss.SendMessageAsync("", false, builder.Build());
            }
        }

        [Command("flipcoin")]
        [Summary("Flips a shitty coin!")]
        public async Task FlipCoinAsync()
        {
            Random random = new Random();
            if (random.Next(0,2) == 0)
            {
                await ReplyAsync("Head");
            }
            else
            {
                await ReplyAsync("Tails");
            }
        }

        [Command("slots")]
        [Summary("GAMBLE ALL YOUR MONEEEEEEY!")]
        public async Task SlotsAsync()
        {

        }
    }
}
