using MAUI.Playkon.ir.V2.Models;

namespace MAUI.Playkon.ir.V2.Helper
{
    public class MediaManagerConverter
    {
        public static MediaItemModel SongToMediaItem(Song song)
        {
            MediaItemModel mediaItem = new MediaItemModel()
            {
                Id = song.id.ToString(),
                MusicId = song.id,
                Album = song.album,
                AlbumArtist = song.artist,
                Artist = song.artist,
                AlbumImageUri = song.cover,
                Author = "Playkon.ir",
                DisplayDescription = song.title,
                DisplayImageUri = song.cover,
                FileExtension = Path.GetExtension(song.name),
                FileName = Path.GetFileName(song.name),
                //Genre = song.genre
                DisplayImage = song.cover,
                DisplayTitle = song.title,
                ImageUri = song.cover,
                MediaUri = song.url,
                Title = song.title,
                Favourite = song.isUserFavorited,
                MusicCount = song.playCount
            };
            if (string.IsNullOrEmpty(song.duration))
                song.duration = "0";
            string durationString = song.duration;
            if (durationString.Contains("."))
                durationString = durationString.Remove(durationString.IndexOf("."));
            var totalSeconds = Convert.ToInt32(durationString);
            TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
            mediaItem.Duration = time;
            return mediaItem;
        }
        public static Song MediaItemToSong(MediaItemModel mediaItem)
        {
            Song song = new Song()
            {
                album = mediaItem.Album,
                artist = mediaItem.Artist,
                cover = mediaItem.ImageUri,
            };
            return song;
        }
        public static List<MediaItemModel> SongListToMediaItemList(List<Song> list)
        {
            int index = 0;
            List<MediaItemModel> mediaItemList = new List<MediaItemModel>();
            foreach (var item in list)
            {
                index++;
                var mediaItem = SongToMediaItem(item);
                if (index < 10)
                    mediaItem.rowCount = "0";
                mediaItem.rowCount += index;
                mediaItemList.Add(mediaItem);
            }
            return mediaItemList;
        }
        public static List<Song> MediaItemListToSongList(List<MediaItemModel> list)
        {
            List<Song> songList = new List<Song>();
            foreach (var item in list)
            {
                songList.Add(MediaItemToSong(item));
            }
            return songList;
        }
    }
}
