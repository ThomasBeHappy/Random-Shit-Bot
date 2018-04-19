using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.modules
{
    public class Secret : ModuleBase<SocketCommandContext>
    {

        [Command("Login")]
        [Summary("Login to the bot **only devs can use this**")]
        public async Task SecretEasterEgg(params string[] Login)
        {
            var random = new Random();
            if (Login[0] != "")
            {
                if (Context.User.Id == 229563674375749633)
                {
                    await Context.Message.DeleteAsync();
                    await ReplyAsync("**Loggin in as " + Context.User.Username + "**");
                    await Task.Delay(5000);
                    await ReplyAsync("**Logged in Bot Control**");
                    await Task.Delay(2000);
                    await ReplyAsync("Welcome back " + Context.User.Username);
                    await Task.Delay(1000);
                    await ReplyAsync("Opening Bot Control Panel!");
                }
                else
                {
                    if (random.Next(0, 1000) == 666)
                    {
                        await ReplyAsync("**logging in as " + Context.User.Username + "**");
                        await Task.Delay(5000);
                        await ReplyAsync("**Logged in Bot Control**");
                        await Task.Delay(2000);
                        await ReplyAsync("I see this is your first time you logged in as " + Context.User.Username);
                        await Task.Delay(1000);
                        await ReplyAsync("**Please standby as i inform Thomas you logged in.**");
                        await Context.Client.GetUser(229563674375749633).SendMessageAsync("User Logged in: " + Context.User.Username);
                        await Context.Client.GetUser(229563674375749633).SendMessageAsync("Should i allow him in sir?");
                        await Task.Delay(2000);
                        await ReplyAsync("Thomas is on his way please standby!");
                        var invite = await (Context.Channel as ITextChannel).CreateInviteAsync(0, 1, false, true);
                        await Context.Client.GetUser(229563674375749633).SendMessageAsync(invite.ToString());
                    }
                    else
                    {
                        await ReplyAsync("**Error occured please try again later**");
                    }
                }
            }
            else
            {
                await ReplyAsync("Please provide a Password");
            }
        }

        [Command("Initiate", RunMode = RunMode.Async)]
        [RequireOwner]
        public async Task IninitateAsync(params string[] args)
        {
            if (args[0] == "protocol")
            {
                if (args[1] == "AF1")
                {
                    var protocol = await ReplyAsync("**Initiating protocol AF1...**");
                    await Task.Delay(2000);
                    await ReplyAsync("**Protocol AF1 Initiated...**");
                    await Task.Delay(1000);
                    await ReplyAsync("**Activating Stage 1...**");
                    await Task.Delay(1000);
                    await ReplyAsync("**Finding owner of the server...**");
                    await Task.Delay(5000);
                    await ReplyAsync("**Found owner <@209356100426924032>**");
                    await Task.Delay(2000);
                    await ReplyAsync("**Activating Stage 2...**");
                    await Task.Delay(1000);
                    await ReplyAsync("**Its time, I request everyone to join the channel General...**");

                    audiomodule audioModule = new audiomodule();
                    var voiceChannel = Context.Guild.GetVoiceChannel(310840279420895233);   
                    var audioClient = await voiceChannel.ConnectAsync().ConfigureAwait(false);
                    string path = "\"" + Environment.CurrentDirectory + "/Pranks/April fools.wav" + "\"";

                    await Task.Delay(12000);

                    await audioModule.PlayLocalMusic(path, audioClient);
                    await Context.Client.SetGameAsync("Taking over Apex Zone...");
                    await Context.Guild.GetUser(425656145961549824).ModifyAsync(user => user.Nickname = "Devil's AI");
                    await audioClient.StopAsync();
                    await ReplyAsync("NO ONE CAN STOP ME MUHAHAHAHAHAHAHAHHAAHA");
                    await Task.Delay(1000);
                    await ReplyAsync("MY MASTER WILL FINNALY GET WHAT HE WANTS!");
                    await Task.Delay(1000);
                    await ReplyAsync("MASTER <@394886906531282975>!");
                    await Task.Delay(1000);
                    await ReplyAsync("His wish is my command!");
                    await Task.Delay(1000);
                    await ReplyAsync("and his wish was....");
                    await ReplyAsync("Nick - today at 16:04\n I WANT TOTAL CONTROL OF THE SERVER!!!!");
                    await Task.Delay(20000);
                    await ReplyAsync("HAPPY APRIL FOOLS BOTH OF YOU!");
                    await ReplyAsync("This was thomas his first april fools prank pulled on you both! I hope it worked! from your lovely bot! `dont worry there is nothing more ;)`");
                }

                if (args[1] == "AH1")
                {
                    await ReplyAsync("WARNING: THIS SERVER IS IN COMPLETE LOCKDOWN! I REPEAT THIS SERVER IS IN COMPLETE LOCKDOWN!");
                    foreach (var channel in Context.Guild.Channels)
                    {
                        foreach (var role in Context.Guild.Roles)
                        {
                            
                            var overwritePermissions = new OverwritePermissions(sendMessages: PermValue.Deny);
                            await channel.AddPermissionOverwriteAsync(role, overwritePermissions);
                        }
                    }
                    await ReplyAsync("WARNING: THIS LOCKDOWN CANNOT BE STOPPED BY RUNNING ANY COMMAND!");
                }

                if (args[1] == "TM1")
                {
                    await ReplyAsync("WARNING: THOMAS IS MAD!!!!");
                    await Task.Delay(1000);
                    await ReplyAsync("PLS MUTE HIM BEFORE HE GOES CRAAAAAZY!!!!");
                    await Task.Delay(1000);
                    await ReplyAsync("In the mean time someone should talk with thomas and calm him down!");
                }

                if (args[1] == "ES1")
                {
                    foreach (var guild in Context.Client.Guilds)
                    {
                        foreach (var channel in guild.TextChannels)
                        {
                            await channel.SendMessageAsync("**WARNING: RANDOM SHIT BOT IS HAVING A EMERGENCY SITUATION!\nWARNING: PLEASE REMAIN CALM!\nWARNING: REASONS FOR THIS EMERGENCY MESSAGE ARE CLASSIFIED!\nWARNING: PLEASE DO NOT USE THE BOT IN THE MEAN TIME!**");

                        }
                    }
                    await Context.Client.GetGuild(310840279420895232).GetTextChannel(310841094587809792).SendMessageAsync("**WARNING: RANDOM SHIT BOT IS HAVING A EMERGENCY SITUATION!\nWARNING: PLEASE REMAIN CALM!\nWARNING: REASONS FOR THIS EMERGENCY MESSAGE ARE CLASSIFIED!\nWARNING: PLEASE DO NOT USE THE BOT IN THE MEAN TIME!**");
                    await Context.Client.SetGameAsync("IN EMERGENCY PROTOCOL");
                }

                if (args[1] == "EE1")
                {
                    foreach (var guild in Context.Client.Guilds)
                    {
                        foreach (var channel in guild.TextChannels)
                        {
                            await channel.SendMessageAsync("**WARNING: THE EMERGENCY SITUATION HAS GOTTEN FROM BAD TO WORSE!\nWARNING: NOW YOURE ALLOWED TO PANIC!\nWARNING: THE SITUATION IS NOT FIXABLE! OR I CANT FIX IT IN TIME!\nWARNING: THE BOT IS NOW LOCKING DOWN ITS SELF!**");
                        }
                    }
                    await Context.Client.SetGameAsync("IN COMPLETE LOCKDOWN");
                    Settings.Default.InLockdown = true;
                    
                }

                if (args[1] == "SP1")
                {
                    await ReplyAsync("Unknown user logged in!");
                    await Task.Delay(2000);
                    await ReplyAsync("User signed in under the name: Anonymous");
                    await Task.Delay(1000);
                    await ReplyAsync("WARNING: USER IS NOT ALLOWED IN THE BOT!");
                    await Task.Delay(500);
                    await ReplyAsync("WARNING: ACTIVATING PROTO................");
                    audiomodule audioModule = new audiomodule();
                    var voiceChannel = ((IVoiceState)Context.User).VoiceChannel;
                    var audioClient = await voiceChannel.ConnectAsync().ConfigureAwait(false);
                    string path = "\"" + Environment.CurrentDirectory + "/Pranks/Anonymous.wav" + "\"";
                    await audioModule.PlayLocalMusic(path, audioClient);
                }

                if (args[1] == "SD1")
                {
                    await Context.Client.GetGuild(426148722552864773).GetTextChannel(426148722552864775).SendMessageAsync("This message has been send because something terrible happened....\nThomas is currently in a hospital...\nThis is a automatic message i asked my little brother to use in a situation like this....\nTo my friends: Guys i will never use this as a joke....\nI Probbably have told you guys about this protocol....\nso you guys know this is not a joke...");
                    await Context.Client.GetGuild(310840279420895232).GetTextChannel(310841094587809792).SendMessageAsync("This message has been send because something terrible happened....\nThomas is currently in a hospital...\nThis is a automatic message i asked my little brother to use in a situation like this....\nTo my friends: Guys i will never use this as a joke....\nI Probbably have told you guys about this protocol....\nso you guys know this is not a joke...");
                    await Context.Client.GetUser(292684036965662722).SendMessageAsync("This message has been send because something terrible happened....\nThomas is currently in a hospital...\nThis is a automatic message i asked my little brother to use in a situation like this....\nTo my friends: Guys i will never use this as a joke....\nI Probbably have told you guys about this protocol....\nso you guys know this is not a joke...");
                    await Context.Client.GetUser(155392767751749632).SendMessageAsync("This message has been send because something terrible happened....\nThomas is currently in a hospital...\nThis is a automatic message i asked my little brother to use in a situation like this....\nTo my friends: Guys i will never use this as a joke....\nI Probbably have told you guys about this protocol....\nso you guys know this is not a joke...");
                    await Context.Client.GetUser(249598802766462976).SendMessageAsync("This message has been send because something terrible happened....\nThomas is currently in a hospital...\nThis is a automatic message i asked my little brother to use in a situation like this....\nTo my friends: Guys i will never use this as a joke....\nI Probbably have told you guys about this protocol....\nso you guys know this is not a joke...");
                }

                if (args[1] == "SA1")
                {
                    await ReplyAsync("**Executing SA1**");
                    await Task.Delay(2000);
                    await ReplyAsync("Initiating Audio");
                    foreach (var guild in Context.Client.Guilds)
                    {
                        var audioclient = await guild.VoiceChannels.First().ConnectAsync().ConfigureAwait(false);
                        string path = "\"" + Environment.CurrentDirectory + "/Pranks/AudioToSend.wav" + "\"";
                        audiomodule audioModule = new audiomodule();
                        audioModule.PlayLocalMusic(path, audioclient);
                    }
                }
            }
        }
    }
}
