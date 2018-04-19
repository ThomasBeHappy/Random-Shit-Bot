using CoolDiscordBot.Misc.Guilds;
using CoolDiscordBot.Misc.useracounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.Misc
{
    class DataStorage
    {
        public static void SaveGuilds(IEnumerable<guild> guilds, string filepath)
        {
            string json = JsonConvert.SerializeObject(guilds, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        public static IEnumerable<guild> LoadGuild(string filepath)
        {
            if (!File.Exists(filepath)) return null;

            string json = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<List<guild>>(json);

        }

        // Economy section
        public static void SaveUserAcounts(IEnumerable<UserAcount> acounts, string filepath)
        {
            string json = JsonConvert.SerializeObject(acounts, Formatting.Indented);
            File.WriteAllText(filepath, json);
        }

        public static IEnumerable<UserAcount> LoadUserAcounts(string filepath)
        {
            if (!File.Exists(filepath)) return null;

            string json = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<List<UserAcount>>(json);

        }

        public static bool SaveExists(string filepath)
        {
            return File.Exists(filepath);
        }
    }
}
