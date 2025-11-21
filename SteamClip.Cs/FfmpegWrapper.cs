using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace SteamClip
{
    public static class FfmpegWrapper
    {
        private static string FFMPEG_PATH = "ffmpeg.exe"; // Assumes ffmpeg is in the PATH

        public static void ExtractFirstFrame(string videoPath, string outputPath)
        {
            var command = $"-i \"{videoPath}\" -ss 00:00:01.000 -vframes 1 \"{outputPath}\"";
            ExecuteFfmpegCommand(command);
        }

        public static void ConvertToMp4(string videoPath, string audioPath, string outputPath)
        {
            var command = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{outputPath}\"";
            ExecuteFfmpegCommand(command);
        }

        private static void ExecuteFfmpegCommand(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = FFMPEG_PATH,
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"ffmpeg exited with code {process.ExitCode}: {error}");
            }
        }
    }
}
