using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.Services;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class PlaylistMusicListViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private string title;

        [ObservableProperty]
        private string cover;

        [ObservableProperty]
        private string musicCount;

        [ObservableProperty]
        private ObservableCollection<MediaItemModel> musicList;

        [RelayCommand]
        private async Task PlayAsync()
        {
            var viewModel = new PlayerViewModel(MusicList.FirstOrDefault(), MusicList);
            var playerPage = new PlayerPage { BindingContext = viewModel };

            await Shell.Current.Navigation.PushAsync(playerPage, true);
        }

        public PlaylistMusicListViewModel(string id, PlaylistType type)
        {
            MusicList = new ObservableCollection<MediaItemModel>();
            Id = id;
            Type = type;

            Task.Run(populateList);
        }

        private void populateList()
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                try
                {
                    switch (Type)
                    {
                        case PlaylistType.Album:
                            var albumResult = ApiService.GetInstance().Post<AlbumMusicListResult>("/Music/AlbumMusicList", "{\"id\":\"" + Id + "\",\"page\":1,\"take\":50}");
                            foreach (var item in albumResult.items)
                                MusicList.Add(MediaManagerConverter.SongToMediaItem(item));
                            MusicCount = albumResult.album.musicCount.ToString();
                            Title = albumResult.album.nameDisplay;
                            Cover = albumResult.album.coverDisplay;
                            break;
                        case PlaylistType.Artist:
                            var artistResult = ApiService.GetInstance().Post<ArtistMusicResult>("/Music/ArtistMusicList", "{\"id\":\"" + Id + "\",\"page\":1,\"take\":50}");
                            foreach (var item in artistResult.items)
                                MusicList.Add(MediaManagerConverter.SongToMediaItem(item));
                            MusicCount = artistResult.artist.musicCount.ToString();
                            Title = artistResult.artist.nameDisplay;
                            Cover = artistResult.artist.coverDisplay;
                            break;
                        case PlaylistType.Playlist:
                            var playlistResult = ApiService.GetInstance().Post<AlbumMusicListResult>("/PlaylistMusic/List",
                                "{\"id\":\"" + Id + "\"}");
                            foreach (var item in playlistResult.items)
                                MusicList.Add(MediaManagerConverter.SongToMediaItem(item));
                            MusicCount = playlistResult.items.Count.ToString();
                            Title = playlistResult.items.FirstOrDefault().title;
                            Cover = playlistResult.items.FirstOrDefault().cover;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
                IsBusy = false;
            });
        }
        public string Id { get; }
        public PlaylistType Type { get; }
    }
    public enum PlaylistType
    {
        Album = 0,
        Artist = 1,
        Playlist = 2
    }
}
