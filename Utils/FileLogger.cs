using System;
using System.IO;

namespace Yt_Downloader;

public class FileLogger : ILogger
{
    public void Log(string message) { }

    public void LogError(string message)
    {
        DateTime time = DateTime.UtcNow;
        File.AppendAllText(GlobalConfigs.LogPath + $"errorLog_{time:d}.txt", message + "\n");
    }

    public void LogWarning(string message) { }
}