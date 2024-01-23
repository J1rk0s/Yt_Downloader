using System;

namespace Yt_Downloader;

public static class GlobalConfigs {
    public static readonly string AppDataFolder =
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/yt_downloader";

    public static readonly string LogPath = GlobalConfigs.AppDataFolder + "/logs/";
}