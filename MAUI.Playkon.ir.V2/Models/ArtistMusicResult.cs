namespace MAUI.Playkon.ir.V2.Models
{
    public class ArtistMusicResult
    {
        public bool status { get; set; }
        public List<Song> items { get; set; }
        public Artist artist { get; set; }
    }
}