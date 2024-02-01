using System;
using System.Collections.ObjectModel;

namespace Yt_Downloader;

public static class GlobalConfigs {
    public static readonly string AppDataFolder =
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/yt_downloader";

    public static readonly string DownloadHistoryFile = GlobalConfigs.AppDataFolder + "/download_history.txt";

    public static readonly string LogPath = GlobalConfigs.AppDataFolder + "/logs/";

    public static readonly ObservableCollection<string> VideoHistory = new();
}