using CoolDiscordBot.Misc.inventory;
using CoolDiscordBot.Misc.useracounts;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Expressions;
using DynamicExpresso;
using CoolDiscordBot.Misc.Guilds;
using CoolDiscordBot.services;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;

namespace CoolDiscordBot.modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        private weatherservice _weatherService;
        public Misc(weatherservice ser)
        {
            _weatherService = ser;
        }

        [Command("bal")]
        [Alias("money","balance")]
        [Summary("Shows how much Shit Bucks you got!")]
        public async Task ShowBallAsync()
        {
            await ReplyAsync("You have " + UserAcounts.GetOrCreateAcount(Context.User.Id).Money + " Shit Bucks!");
        }

        [Command("givebucks")]
        [Summary("Give some shit bucks to someone else and make them a Shit Holder!")]
        public async Task GiveBallAsync(params string[] args)
        {
            UserAcount userAcount = UserAcounts.GetOrCreateAcount(Context.Message.MentionedUsers.First().Id);
            UserAcount userAcount2 = UserAcounts.GetOrCreateAcount(Context.User.Id);

            if (userAcount2.Money >= Convert.ToUInt32(args[1]))
            {
                userAcount.Money += Convert.ToUInt32(args[1]);
                userAcount2.Money -= Convert.ToUInt32(args[1]);

                await ReplyAsync(Context.User.Username + " Just gave " + Context.Message.MentionedUsers.First().Username + " " + args[1] + " Shit Bucks");
                UserAcounts.SaveAcounts();
            }
            else
            {
                await ReplyAsync("Not enough Shitty Bucks!");
            }
        }


        [Command("daily")]
        [Summary("Get your daily shit!")]
        public async Task GetDailyShit()
        {
            UserAcount userAcount = UserAcounts.GetOrCreateAcount(Context.User.Id);

            if (DateTime.Now >= userAcount.DailyBucksReset)
            {
                userAcount.DailyBucksReset = DateTime.Now.AddDays(1);
                userAcount.Money += 200;
                await ReplyAsync("**FART**, Here is your daily Shit! **200 shit bucks received!**");
                UserAcounts.SaveAcounts();
            }
            else
            {
                TimeSpan Remaining = userAcount.DailyBucksReset - DateTime.Now;
                await ReplyAsync("Sorry! i cant give it to you yet! i have not taken a shit yet! You gotta wait: " + Remaining.ToString(@"hh\:mm\:ss"));
            }
        }

        [Command("Work")]
        public async Task WorkAsync(params string[] work)
        {
            Random random = new Random();
            UserAcount userAcount = UserAcounts.GetOrCreateAcount(Context.User.Id);

            if (DateTime.Now >= userAcount.WorkReset)
            {
                userAcount.WorkReset = DateTime.Now.AddMinutes(30);
                uint Salaris = Convert.ToUInt32(random.Next(10, 50));
                userAcount.Money += Salaris;
                await ReplyAsync("You just worked and received " + Salaris + " Shit bucks!");
                UserAcounts.SaveAcounts();
            }
            else
            {
                TimeSpan Remaining = userAcount.WorkReset - DateTime.Now;
                await ReplyAsync("You cant work yet you gotta take a pause! You have to wait: " + Remaining.ToString(@"hh\:mm\:ss"));

            }
        }





        [Command("inventory")]
        [Summary("Shows your inventory!")]
        [Alias("i","inv")]
        public async Task OpenInventoryAsync()
        {
            var inventory = UserAcounts.GetAcount(Context.User);

            EmbedBuilder builder = new EmbedBuilder();

            foreach (var item in inventory.inventory)
            {
                builder.AddInlineField("Item", item.name + " | " + item.description + Environment.NewLine);
            }

            await ReplyAsync("", false, builder.Build());
        }

        [Command("additem")]
        [Summary("Adds a item to your inventory! only the **owner** can use this right now!")]
        [RequireOwner]
        public async Task additemasync(params string[] args)
        {
            var inventory = UserAcounts.GetOrCreateAcount(Context.User.Id);


            foreach (var word in args)
            {

            }

            Item item = new Item();
            item.name = args[0];
            item.description = "";
            item.costs = Convert.ToUInt64(args[args.Length - 1]);
            inventory.inventory.Add(item);

            UserAcounts.SaveAcounts();
        }

        [Command("eval")]
        [Summary("Very Dangerous!!! only the **owner** of the bot can use this!")]
        [RequireOwner]
        public async Task StartGameAsync(params string[] code)
        {
            Interpreter interpreter = new Interpreter();
            var result = interpreter.Eval(Context.Message.Content.Substring(5), new Parameter("Context", Context));
            await ReplyAsync(result.ToString());

        }

        [Command("userinfo")]
        [Summary("Get some info about a user!")]
        public async Task UserinfoAsync(params string[] users)
        {
            if (Context.Message.MentionedUsers != null)
            {
                var user = Context.Message.MentionedUsers.First();
                var user2 = user as SocketGuildUser;
                EmbedBuilder builder = new EmbedBuilder();
                builder.WithThumbnailUrl(user.GetAvatarUrl());
                builder.Title = user.Username + "#" + user.Discriminator;
                builder.AddField("Username", user.Username);
                builder.AddField("ID", user.Id);
                string roles = "";
                foreach (var role in user2.Roles)
                {
                    roles = roles + role.Name + ", ";
                }
                roles.Substring(0, roles.Length - 2);
                builder.AddField("Rank", roles);
                string permissions = "";
                foreach (var permission in user2.GuildPermissions.ToList())
                {
                    permissions = permissions + permission.ToString() + ", ";
                }
                permissions.Substring(0, roles.Length - 2);
                builder.AddField("Permissions", permissions);

                await ReplyAsync("", false, builder.Build());

            }
            else
            {
                await ReplyAsync("Couldn't find user. please use @usernamehere in order to make this work!");
            }

        }

        [Command("prefix")]
        [Summary("set the prefix!")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task setprefixasync(string prefix)
        {
            var config = Guilds.getorcreateguild(Context.Guild);

            config.prefix = prefix;
        }

        [Command("weather", RunMode = RunMode.Async), Summary("Gets the weather of the specified city")]
        public async Task GetWeather(params string[] query)
        {
            await _weatherService.GetWeather(Context, String.Join(" ", query));
        }

        [Command("stats")]
        [Summary("Gives info about the bot!")]
        public async Task GetStatsAsync()
        {
            var proc = Process.GetCurrentProcess();

            DiscordSocketClient _client = Context.Client as DiscordSocketClient;
            Func<double, double> formatRamValue = d =>
            {
                while (d > 1024)
                    d /= 1024;

                return d;
            };

            Func<long, string> formatRamUnit = d =>
            {
                var units = new string[] { "B", "kB", "mB", "gB" };
                var unitCount = 0;
                while (d > 1024)
                {
                    d /= 1024;
                    unitCount++;
                }

                return units[unitCount];
            };
            double VSZ = 0;
            double RSS = 0;
            try
            {
                if (File.Exists($"/proc/{proc.Id}/statm"))
                {
                    var ramusageInitial = File.ReadAllText($"/proc/{proc.Id}/statm");
                    var ramusage = ramusageInitial.Split(' ')[0];
                    VSZ = double.Parse(ramusage);
                    VSZ = VSZ * 4096 / 1048576;
                    ramusage = ramusageInitial.Split(' ')[1];
                    RSS = double.Parse(ramusage);
                    RSS = RSS * 4096 / 1048576;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var ebn = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                },
                Title = "**Random Shit Bot Info**"
            };
            ebn.AddField((x) =>
            {
                x.Name = "Uptime";
                x.IsInline = true;
                x.Value = (DateTime.Now - proc.StartTime).ToString(@"d'd 'hh\:mm\:ss");
            });

            ebn.AddField((x) =>
            {
                x.Name = ".NET Framework";
                x.IsInline = true;
                x.Value = RuntimeInformation.FrameworkDescription;
            });

            ebn.AddField((x) =>
            {
                x.Name = "Used RAM";
                x.IsInline = true;
                x.Value = $"{(proc.PagedMemorySize64 == 0 ? $"{RSS.ToString("f1")} mB / {VSZ.ToString("f1")} mB" : $"{formatRamValue(proc.PagedMemorySize64).ToString("f2")} {formatRamUnit(proc.PagedMemorySize64)} / {formatRamValue(proc.VirtualMemorySize64).ToString("f2")} {formatRamUnit(proc.VirtualMemorySize64)}")}";
            });
            ebn.AddField((x) =>
            {
                x.Name = "Threads running";
                x.IsInline = true;
                x.Value = $"{((IEnumerable)proc.Threads).OfType<ProcessThread>().Where(t => t.ThreadState == ThreadState.Running).Count()} / {proc.Threads.Count}";
            });
            ebn.AddField((x) =>
            {
                x.Name = "Connected Guilds";
                x.IsInline = true;
                x.Value = $"{_client.Guilds.Count}";
            });
            var channelCount = 0;
            var userCount = 0;
            foreach (var g in _client.Guilds)
            {
                channelCount += g.Channels.Count;
                userCount += g.MemberCount;
            }
            ebn.AddField((x) =>
            {
                x.Name = "Watching Channels";
                x.IsInline = true;
                x.Value = $"{channelCount}";
            });
            ebn.AddField((x) =>
            {
                x.Name = "Users with access";
                x.IsInline = true;
                x.Value = $"{userCount}";
            });
            ebn.AddField((x) =>
            {
                x.Name = "Ping";
                x.IsInline = true;
                x.Value = $"{_client.Latency} ms";
            });

            await Context.Channel.SendMessageAsync("", false, ebn);
        }

        [Command("Version")]
        public async Task GetVersionAsync()
        {
            await ReplyAsync("I am running Version: " + Assembly.GetExecutingAssembly().GetName().Version);
        }

        [Command("Changelog")]
        [RequireOwner]
        public async Task GetChangeLogAsync()
        {
            string filepath = "Changelog.txt";
            string ChangeLog = File.ReadAllText(filepath);
            await ReplyAsync("Changelog: " + ChangeLog);
        }

        [Command("Futureplans")]
        public async Task GetFutureplans()
        {
            string filepath = "Plans.txt";
            string plans = File.ReadAllText(filepath);
            await ReplyAsync("Future Plans for me: " + plans);
        }

        [Command("Credits")]
        public async Task GetCreditsAsync()
        {
            string filepath = "Credits.txt";
            string Credits = File.ReadAllText(filepath);
            await ReplyAsync(Credits);
        }
    }
}
