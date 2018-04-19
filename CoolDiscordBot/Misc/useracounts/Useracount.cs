using CoolDiscordBot.Misc.inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoolDiscordBot.Misc.useracounts
{
    public class UserAcount
    {
        public ulong ID { get; set; }

        public uint Money { get; set; }
        
        public DateTime DailyBucksReset { get; set; }
        public DateTime WorkReset { get; set; }

        public List<Item> inventory { get; set; }
    }
}
