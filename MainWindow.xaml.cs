using System;
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
    }

    private async void OnDownloadClick(object sender, RoutedEventArgs e) {
        Button? btn = sender as Button;
        btn!.IsEnabled = false;
        this.downloadText.Visibility = Visibility.Visible;

        string path = YtDownloader.GetDownloadPath();

        CustomControls.LoadBlur blur = new();
        blur.Opacity = 0;
        DoubleAnimation da = new(1.0d, TimeSpan.FromMilliseconds(500));
        this.MainGrid.Children.Add(blur);
        blur.BeginAnimation(Grid.OpacityProperty, da);

        switch (this.urlType.SelectedIndex) {
            case 0:
                await YtDownloader.DownloadPlaylist(path, this.url.Text ,this.Format.SelectedIndex, blur.progress, new FileLogger());
                break;
            case 1:
                await YtDownloader.DownloadVideo(path, this.url.Text ,this.Format.SelectedIndex, new FileLogger());
                break;
        }

        this.MainGrid.Children.Remove(blur);

        btn.IsEnabled = true;
        this.downloadText.Visibility = Visibility.Hidden;

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