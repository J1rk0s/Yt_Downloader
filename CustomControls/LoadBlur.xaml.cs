using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Yt_Downloader.CustomControls;
/// <summary>
/// Interakční logika pro LoadBlur.xaml
/// </summary>
public partial class LoadBlur : UserControl {
    private ProgressBar progressBar;
    private CancellationTokenSource _tokenSource;

    public LoadBlur(ref CancellationTokenSource tokenSource) {
        this.InitializeComponent();
        this.progressBar = this.Progress;
        this.Opacity = 0;
        this._tokenSource = tokenSource;
    }

    private void CancelButtonClick(object sender, RoutedEventArgs e) {
        this._tokenSource.Cancel();
        this._tokenSource.Dispose();
    }

    private void ProgressValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
        this.ProgressContent.Text = $"{e.NewValue}/{this.Progress.Maximum}";
    }
}
