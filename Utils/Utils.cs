namespace Yt_Downloader;

public static class Utils {
    public static string ParseVideoName(string videoName) {
        string[] badChars = { "<", ">", ":", "\"", "/", "\\", "|", "?", "*", "~", };
        foreach (string badChar in badChars) {
            if (videoName.Contains(badChar)) videoName = videoName.Replace(badChar, "x");
        }

        return videoName;
    }
}