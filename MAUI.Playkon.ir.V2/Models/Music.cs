using SQLite;

namespace MAUI.Playkon.ir.V2.Models
{
    public class Music
    {
        [PrimaryKey]
        public string id { get; set; }
        public string url { get; set; }
        public string tags { get; set; }
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
        public string musicBitrate { get; set; }
        public string cover { get; set; }
        public string artwork { get; set; }
    }
}
