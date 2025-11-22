using System;
using System.IO;

namespace SteamClip
{
    public static class Logger
    {
        private static string logFilePath;

        public static void Initialize(string configDir)
        {
            var logDir = Path.Combine(configDir, "logs");
            Directory.CreateDirectory(logDir);
            logFilePath = Path.Combine(logDir, $"log-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
        }

        public static void Info(string message)
        {
            Log("INFO", message);
        }

        public static void Error(string message, Exception ex = null)
        {
            var errorMessage = ex != null ? $"{message}: {ex}" : message;
            Log("ERROR", errorMessage);
        }

        private static void Log(string level, string message)
        {
            if (logFilePath == null)
            {
                // Logger not initialized, cannot log.
                return;
            }
            try
            {
                File.AppendAllText(logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}{Environment.NewLine}");
            }
            catch (Exception)
            {
                // Ignore logging errors.
            }
        }
    }
}
