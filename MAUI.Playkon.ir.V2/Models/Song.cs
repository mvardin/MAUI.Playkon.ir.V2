namespace MAUI.Playkon.ir.V2.Models
{
    public class Song
    {
        public string rowCount { get; set; }
        public Guid id { get; set; }
        public string url { get; set; }
        public List<string> tags { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string extension { get; set; }
        public string musicSize { get; set; }
        public string musicAlbum { get; set; }
        public string album { get; set; }
        public string musicAlbumId { get; set; }
        public string musicArtist { get; set; }
        public string artist { get; set; }
        public string musicArtistId { get; set; }
        public string musicDuration { get; set; }
        public string duration { get; set; }
        public string durationDisplay
        {
            get
            {
                try
                {
                    string durationString = duration;
                    if (durationString.Contains("."))
                        durationString = durationString.Remove(durationString.IndexOf("."));
                    var totalSeconds = Convert.ToInt32(durationString);
                    TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
                    string str = time.ToString(@"mm\:ss");
                    return str;
                }
                catch (Exception ex)
                {
                    return "0";
                }
            }
        }
        public string musicBitrate { get; set; }
        public object musicGenre { get; set; }
        public string genre { get; set; }
        public object playlist { get; set; }
        public string cover { get; set; }
        public string artwork { get; set; }
        public object playlistMusicId { get; set; }
        public int playCount { get; set; }
        public int favoriteCount { get; set; }
        public bool isUserFavorited { get; set; }
    }

    public class SongResult
    {
        public bool status { get; set; }
        public string timeElapsed { get; set; }
        public List<Song> items { get; set; }
    }
}

