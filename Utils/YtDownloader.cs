using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Playlists;
using YoutubeExplode.Search;
using YoutubeExplode.Videos.Streams;

namespace Yt_Downloader;
public class YtDownloader {
    private static readonly YoutubeClient _client = new();

    public static async Task DownloadPlaylist(string path, string vidUrl, int type, ProgressBar progress, ILogger? logger = null) {
        try {
            string ext = type == 0 ? "mp3" : "mp4";
            IReadOnlyList<PlaylistVideo> videos = await _client.Playlists.GetVideosAsync(vidUrl);
            progress.Value = 0;
            progress.Maximum = videos.Count;

            foreach (PlaylistVideo video in videos) {
                try {
                    StreamManifest manifest = await _client.Videos.Streams.GetManifestAsync(video.Url);
                    IStreamInfo info = YtDownloader.GetStreamInfoFromType(type, manifest);

                    await _client.Videos.Streams.DownloadAsync(info,
                        $"{path}/{Utils.ParseVideoName(video.Title)}.{ext}");
                    progress.Value++;
                } catch (Exception e) {
                    MessageBox.Show("Error occured but continuing! Saving to log");
                    logger?.LogError(e.ToString());
                    progress.Value++;
                }

            }
        } catch (PlaylistUnavailableException) {
            MessageBox.Show("Playlist is not available!");
        } catch (Exception e) {
            MessageBox.Show($"Error: {e.Message}\nSaving to log");
            logger?.LogError(e.ToString());
        }
    }

    public static async Task DownloadVideo(string path, string vidUrl, int type, ILogger? logger = null) {
        try {
            string ext = type == 0 ? "mp3" : "mp4";
            StreamManifest manifest = await _client.Videos.Streams.GetManifestAsync(vidUrl);
            IStreamInfo info = YtDownloader.GetStreamInfoFromType(type, manifest);

            IReadOnlyList<VideoSearchResult> result = await _client.Search.GetVideosAsync(vidUrl);
            await _client.Videos.Streams.DownloadAsync(info, $"{path}/{Utils.ParseVideoName(result[0].Title)}.{ext}");
        } catch (Exception e) {
            MessageBox.Show($"Error: {e.Message}\nSaving to log");
            logger?.LogError(e.ToString());
        }
    }

    public static string GetDownloadPath() {
        VistaFolderBrowserDialog dialog = new() {
            UseDescriptionForTitle = true,
            Description = "Please select a folder that the videos will be downloaded to",
            ShowNewFolderButton = true,
            Multiselect = false,
            RootFolder = Environment.SpecialFolder.MyVideos,
        };

        if (dialog.ShowDialog() == true) return dialog.SelectedPath;

        MessageBox.Show("Defaulting to Videos folder");
        return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
    }

    private static IStreamInfo GetStreamInfoFromType(int type, StreamManifest manifest) {
        return type switch {
            0 => manifest.GetAudioOnlyStreams().GetWithHighestBitrate(),
            1 => manifest.GetMuxedStreams().GetWithHighestVideoQuality(),
            var _ => manifest.GetAudioOnlyStreams().GetWithHighestBitrate(),
        };
    }
}
