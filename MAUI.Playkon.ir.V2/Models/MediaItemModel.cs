using MediaManager.Library;
using MediaManager.Media;
using System.ComponentModel;

namespace MAUI.Playkon.ir.V2.Models
{
    public class MediaItemModel : IMediaItem
    {
        public Guid MusicId { get; set; }
        public bool Favourite { get; set; }
        public string rowCount { get; set; }
        public int MusicCount { get; set; }
        public string TatalDuration
        {
            get
            {
                return this.Duration.ToString(@"mm\:ss");
            }
        }

        public bool IsMetadataExtracted { get; set; }
        public string Advertisement { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public object AlbumImage { get; set; }
        public string AlbumImageUri { get; set; }
        public string Artist { get; set; }
        public object Image { get; set; }
        public string ImageUri { get; set; }
        public string Author { get; set; }
        public string Compilation { get; set; }
        public string Composer { get; set; }
        public DateTime Date { get; set; }
        public int DiscNumber { get; set; }
        public object DisplayImage { get; set; }
        public string DisplayImageUri { get; set; }
        public string DisplayDescription { get; set; }
        public string DisplaySubtitle { get; set; }
        public string DisplayTitle { get; set; }
        public DownloadStatus DownloadStatus { get; set; }
        public TimeSpan Duration { get; set; }
        public object Extras { get; set; }
        public string Genre { get; set; }
        public string MediaUri { get; set; }
        public int NumTracks { get; set; }
        public object Rating { get; set; }
        public string Title { get; set; }
        public int TrackNumber { get; set; }
        public object UserRating { get; set; }
        public string Writer { get; set; }
        public int Year { get; set; }
        public string FileExtension { get; set; }
        public string FileName { get; set; }
        public MediaType MediaType { get; set; }
        public MediaLocation MediaLocation { get; set; }
        public bool IsLive { get; set; }
        public Stream Data { get; set; }
        public MimeType MimeType { get; set; }
        public string Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        event EventHandler<MetadataChangedEventArgs> IMediaItem.MetadataUpdated { add { } remove { } }
    }
}
