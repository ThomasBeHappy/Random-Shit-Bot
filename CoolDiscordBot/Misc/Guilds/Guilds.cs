using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.Misc.Guilds
{
    public static class Guilds
    {
        private static List<guild> guilds;

        private static string acountsfile = "guilds.json";

        static Guilds()
        {
            if (DataStorage.SaveExists(acountsfile))
            {
                guilds = DataStorage.LoadGuild(acountsfile).ToList();
            }
            else
            {
                guilds = new List<guild>();
                saveguilds();
            }

        }

        public static void saveguilds()
        {
            DataStorage.SaveGuilds(guilds, acountsfile);

        }


        public static guild GetAcount(SocketGuild guild)
        {
            return getorcreateguild(guild);
        }

        public static guild getorcreateguild(SocketGuild guild)
        {
            var result = from a in guilds
                         where a.ID == guild.Id
                         select a;
            var acount = result.FirstOrDefault();
            if (acount == null) acount = CreateGuild(guild);

            return acount;
        }

        private static guild CreateGuild(SocketGuild guild)
        {
            var newguild = new guild()
            {
                ID = guild.Id,
                Name = guild.Name,
                prefix = "!"
            };

            guilds.Add(newguild);
            saveguilds();
            return newguild;

        }
    }
}
