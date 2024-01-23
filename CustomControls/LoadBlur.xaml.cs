using System.Windows;
using System.Windows.Controls;

namespace Yt_Downloader.CustomControls;
/// <summary>
/// Interakční logika pro LoadBlur.xaml
/// </summary>
public partial class LoadBlur : UserControl {
    private ProgressBar progressBar;

    public LoadBlur() {
        this.InitializeComponent();
        this.progressBar = this.progress;
        this.Opacity = 0;
    }
}
