namespace MAUI.Playkon.ir.V2.Models
{
    public class AlbumMusicListResult
    {
        public bool status { get; set; }
        public List<Song> items { get; set; }
        public Album album { get; set; }
    }
}