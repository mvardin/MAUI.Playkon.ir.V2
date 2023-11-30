namespace MAUI.Playkon.ir.V2.Models
{
    public class UserPlaylist
    {
        public string cover { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public int musicCount { get; set; }
        public string searchQuery { get; set; }
        public string type { get; set; }
    }

    public class UserPlaylistResult
    {
        public bool status { get; set; }
        public string timeElapsed { get; set; }
        public List<UserPlaylist> items { get; set; }
    }


}
