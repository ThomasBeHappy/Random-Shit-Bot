using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.services
{
    public class weatherservice
    {
        public async Task GetWeather(SocketCommandContext Context, string query)
        {
            try
            {
                var search = System.Net.WebUtility.UrlEncode(query);
                string response = "";
                using (var http = new HttpClient())
                {
                    response = await http.GetStringAsync($"http://api.openweathermap.org/data/2.5/weather?q={search}&appid=27efcb877cbdcccb03a5b09b15540d52&units=metric").ConfigureAwait(false);
                }
                var data = JsonConvert.DeserializeObject<WeatherData>(response);

                await Context.Channel.SendMessageAsync("", embed: data.GetEmbed());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Context.Channel.SendMessageAsync("Couldn't find weather for that city!");
            }

        }
    }
}
