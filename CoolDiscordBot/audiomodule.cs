using Discord.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolDiscordBot.modules
{
    public class audiomodule
    {


        //if you want to play only local files, call the PlayLocalMusic function with this path
        //string path = "\"" + Environment.CurrentDirectory + "/something.mp3" + "\"";

        public async Task PlayLocalMusic(string path, IAudioClient client)
        {
            try
            {
                byte[] buffer = new byte[3840];
                int bytesRead = 0;

                var audioStream = client.CreatePCMStream(AudioApplication.Music, bufferMillis: 1920);
                var p = CreateStream(path);
                var _outStream = p.StandardOutput.BaseStream;

                while ((bytesRead = _outStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    await audioStream.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
                }
                await audioStream.FlushAsync().ConfigureAwait(false);
            }
            catch
            {

            }
        }

        private Process CreateStream(string path)
        {
            var args = $"-err_detect ignore_err -i {path} -f s16le -ar 48000 -vn -ac 2 pipe:1 -loglevel error";
            /*if (!_isLocal)
            args = "-reconnect 1 -reconnect_streamed 1 -reconnect_delay_max 5 " + args;*/

            return Process.Start(new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = false,
                CreateNoWindow = true,
            });
        }
    }
}
