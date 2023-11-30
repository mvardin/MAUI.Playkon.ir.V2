using MAUI.Playkon.ir.V2.Models;
using SQLite;

namespace MAUI.Playkon.ir.V2.Data
{
    public class MusicData
    {
        private SQLiteAsyncConnection _database;
        public MusicData()
        {
            _database = new SqliteManager().GetContext();
        }

        public void Add(Music data)
        {
            _ = _database.InsertAsync(data);
        }
        public void Add(Song song)
        {
            Music data = new Music()
            {
                album = song.album,
                artist = song.artist,
                artwork = song.artwork,
                cover = song.cover,
                duration = song.duration,
                extension = song.extension,
                id = song.id.ToString(),
                musicAlbum = song.musicAlbum,
                musicArtist = song.musicArtist,
                musicAlbumId = song.musicAlbumId,
                musicArtistId = song.musicArtistId,
                musicBitrate = song.musicBitrate,
                musicDuration = song.musicDuration,
                musicSize = song.musicSize,
                name = song.name,
                tags = song.tags.FirstOrDefault(),
                title = song.title,
                url = song.url
            };
            _ = _database.InsertAsync(data);
        }
        public AsyncTableQuery<Music> List()
        {
            return _database.Table<Music>();
        }
        public void Delete(Music data)
        {
            _ = _database.DeleteAsync(data);
        }
    }
}