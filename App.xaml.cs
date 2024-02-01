using System.IO;
using System.Windows;

namespace Yt_Downloader;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    public App() {
        if (!Directory.Exists(GlobalConfigs.AppDataFolder)) Directory.CreateDirectory(GlobalConfigs.AppDataFolder);

        if (!Directory.Exists(GlobalConfigs.LogPath)) Directory.CreateDirectory(GlobalConfigs.LogPath);
    }

}