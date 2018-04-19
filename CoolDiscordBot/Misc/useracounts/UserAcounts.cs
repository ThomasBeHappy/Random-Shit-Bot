using CoolDiscordBot.Misc.inventory;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.Misc.useracounts
{
    public static class UserAcounts
    {
        private static List<UserAcount> acounts;

        private static string acountsfile = "acounts.json";

        static UserAcounts()
        {
            if (DataStorage.SaveExists(acountsfile))
            {
                acounts = DataStorage.LoadUserAcounts(acountsfile).ToList();
            }
            else
            {
                acounts = new List<UserAcount>();
                SaveAcounts();
            }

        }

        public static void SaveAcounts()
        {
            DataStorage.SaveUserAcounts(acounts, acountsfile);

        }


        public static UserAcount GetAcount(SocketUser user)
        {
            return GetOrCreateAcount(user.Id);
        }

        public static UserAcount GetOrCreateAcount(ulong id)
        {
            var result = from a in acounts
                         where a.ID == id
                         select a;
            var acount = result.FirstOrDefault();
            if (acount == null) acount = CreateAcount(id);

            return acount;
        }

        private static UserAcount CreateAcount(ulong id)
        {
            var newAcount = new UserAcount()
            {
                ID = id,
                Money = 0,
                DailyBucksReset = DateTime.Now,
                WorkReset = DateTime.Now,
                inventory = new List<Item>()
            };

            acounts.Add(newAcount);
            SaveAcounts();
            return newAcount;

        }
    }
}
