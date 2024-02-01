using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Yt_Downloader;

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        this.InitializeComponent();

        this.DownloadHistory.Text = string.Join("\n", GlobalConfigs.VideoHistory);
        GlobalConfigs.VideoHistory.CollectionChanged += (sender, args) => {
            this.DownloadHistory.Text = string.Join("\n", GlobalConfigs.VideoHistory);
        };
    }

    private async void OnDownloadClick(object sender, RoutedEventArgs e) {
        Button? btn = sender as Button;
        btn!.IsEnabled = false;

        string path = YtDownloader.GetDownloadPath();
        
        CancellationTokenSource tokenSource = new();

        CustomControls.LoadBlur blur = new(ref tokenSource) {
            Opacity = 0,
        };
        DoubleAnimation da = new(1.0d, TimeSpan.FromMilliseconds(500));
        this.MainGrid.Children.Add(blur);
        blur.BeginAnimation(Grid.OpacityProperty, da);

        switch (this.urlType.SelectedIndex) {
            case 0:
                await YtDownloader.DownloadPlaylist(path, this.url.Text ,this.Format.SelectedIndex, blur.Progress, tokenSource.Token, new FileLogger());
                break;
            case 1:
                await YtDownloader.DownloadVideo(path, this.url.Text ,this.Format.SelectedIndex, tokenSource.Token, new FileLogger());
                break;
        }

        this.MainGrid.Children.Remove(blur);

        btn.IsEnabled = true;

        System.Diagnostics.Process.Start("explorer.exe", path);
    }

    private void Minimize(object sender, RoutedEventArgs e) {
        this.WindowState = WindowState.Minimized;
    }

    private void DragWindow(object sender, MouseButtonEventArgs e) {
        this.DragMove();
    }

    private void CloseWindow(object sender, RoutedEventArgs e) {
        this.Close();
    }
}