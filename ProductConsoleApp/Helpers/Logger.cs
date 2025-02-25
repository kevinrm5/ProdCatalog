using System;

namespace ProductConsoleApp.Helpers
{
    public static class Logger
    {
        public static void LogError(string message)
        {
            string logFilePath = "error_log.txt";
            string logEntry = $"{DateTime.UtcNow}: {message}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logEntry);
            Console.WriteLine($"Error logged: {message}");
        }
    }
}
