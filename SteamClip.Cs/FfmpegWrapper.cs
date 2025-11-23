using System;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace SteamClip
{
    public static class FfmpegWrapper
    {
        private static string ffmpegPath = "ffmpeg.exe"; // Default to PATH
        private const int TIMEOUT_MS = 30000; // 30 seconds timeout

        public static void SetFfmpegPath(string path)
        {
            ffmpegPath = path;
        }

        private static string GetFfmpegPath()
        {
            var bundledFfmpeg = Path.Combine(Application.StartupPath, "ffmpeg.exe");
            if (File.Exists(bundledFfmpeg))
            {
                return bundledFfmpeg;
            }
            if (!string.IsNullOrEmpty(ffmpegPath) && File.Exists(ffmpegPath))
            {
                return ffmpegPath;
            }
            return "ffmpeg.exe"; // Fallback to PATH
        }

        public static void ExtractFirstFrame(string videoPath, string outputPath)
        {
            var command = $"-i \"{videoPath}\" -ss 00:00:01.000 -vframes 1 \"{outputPath}\"";
            ExecuteFfmpegCommand(command, CancellationToken.None);
        }

        public static void ConvertToMp4(string videoPath, string audioPath, string outputPath, CancellationToken token)
        {
            var command = $"-i \"{videoPath}\" -i \"{audioPath}\" -c:v copy -c:a aac \"{outputPath}\"";
            ExecuteFfmpegCommand(command, token);
        }

        private static void ExecuteFfmpegCommand(string command, CancellationToken token)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = GetFfmpegPath(),
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

                while (!process.WaitForExit(100))
                {
                    if (token.IsCancellationRequested)
                    {
                        process.Kill();
                        token.ThrowIfCancellationRequested();
                    }
                }

                if (process.ExitCode != 0)
                {
                    throw new Exception($"ffmpeg exited with code {process.ExitCode}. Command: {command}. Error: {errorBuilder}");
                }
            }
            catch (Win32Exception ex)
            {
                throw new Exception($"Could not start ffmpeg. Make sure ffmpeg.exe is bundled, configured in settings, or in the system's PATH. Command: {command}", ex);
            }
        }
    }
}
