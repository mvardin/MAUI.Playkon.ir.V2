namespace MAUI.Playkon.ir.V2.Models
{
    public class Artist
    {
        public string id { get; set; }
        public string pArtistId { get; set; }
        public string name { get; set; }
        public string nameDisplay { get; set; }
        public string cover { get; set; }
        public string coverDisplay { get; set; }
        public int musicCount { get; set; }
    }

    public class ArtistResult
    {
        public bool status { get; set; }
        public string timeElapsed { get; set; }
        public List<Artist> items { get; set; }
    }
}
