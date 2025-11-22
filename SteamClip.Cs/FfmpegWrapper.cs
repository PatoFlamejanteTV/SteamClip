using System;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SteamClip
{
    public static class FfmpegWrapper
    {
        private static string FFMPEG_PATH = "ffmpeg.exe"; // Assumes ffmpeg is in the PATH
        private const int TIMEOUT_MS = 30000; // 30 seconds timeout

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
                },
                EnableRaisingEvents = true
            };

            var errorBuilder = new StringBuilder();
            process.ErrorDataReceived += (sender, args) =>
            {
                if (args.Data != null)
                {
                    errorBuilder.AppendLine(args.Data);
                }
            };

            try
            {
                process.Start();
                process.BeginErrorReadLine();

                if (!process.WaitForExit(TIMEOUT_MS))
                {
                    process.Kill();
                    throw new Exception($"ffmpeg process timed out after {TIMEOUT_MS / 1000} seconds. Command: {command}");
                }

                if (process.ExitCode != 0)
                {
                    throw new Exception($"ffmpeg exited with code {process.ExitCode}. Command: {command}. Error: {errorBuilder}");
                }
            }
            catch (Win32Exception ex)
            {
                throw new Exception($"Could not start ffmpeg. Make sure ffmpeg.exe is in the system's PATH. Command: {command}", ex);
            }
        }
    }
}
