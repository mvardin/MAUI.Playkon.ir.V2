using Android.Provider;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.Services;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class HomeViewModel : ObservableObject, IRecipient<CurrentMusicMessageModel>
    {
        [ObservableProperty]
        private ObservableCollection<MediaItemModel> recentMusicList;
        [ObservableProperty]
        private bool isMusicLoading = true;

        [ObservableProperty]
        private ObservableCollection<Models.Album> recentAlbumList;
        [ObservableProperty]
        private bool isAlbumsLoading = true;

        [ObservableProperty]
        private ObservableCollection<Models.Artist> recentArtistList;
        [ObservableProperty]
        private bool isArtistLoading = true;

        [ObservableProperty]
        private MediaItemModel selectedMusic;

        public HomeViewModel()
        {
            StrongReferenceMessenger.Default.Register(this);
            Task.Run(GetMusics);
            Task.Run(GetAlbums);
            Task.Run(GetArtists);
        }

        [RelayCommand]
        private async void Selection()
        {
            if (SelectedMusic != null)
            {
                //var viewModel = new PlayerViewModel(SelectedMusic, RecentMusicList);
                //var playerPage = new PlayerPage { BindingContext = viewModel };

                StrongReferenceMessenger.Default.Send(new CurrentMusicMessageModel()
                {
                    Music = SelectedMusic,
                    PlayNewInstance = true,
                    MusicList = recentMusicList
                });

                //Shell.Current.Navigation.PushAsync(playerPage, true);
            }
        }

        public async Task GetMusics()
        {
            IsMusicLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var songResult = ApiService.GetInstance().Post<SongResult>("/Music/Recent", "{\"page\":1,\"take\":50}");
                    var recentMusicList = new ObservableCollection<MediaItemModel>();
                    var mediaItemList = MediaManagerConverter.SongListToMediaItemList(songResult.items);
                    foreach (var song in mediaItemList)
                    {
                        recentMusicList.Add(song);
                    }
                    RecentMusicList = recentMusicList;
                }
                catch (System.Exception ex)
                {
                    //show error
                }
                IsMusicLoading = false;
            });
        }
        public async Task GetAlbums()
        {
            IsAlbumsLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var albums = ApiService.GetInstance().Post<AlbumResult>("/Music/Album", "{\"page\":1,\"take\":10}");
                    var recentAlbumList = new ObservableCollection<Models.Album>();
                    foreach (var item in albums.items)
                        recentAlbumList.Add(item);
                    RecentAlbumList = recentAlbumList;
                }
                catch (System.Exception ex)
                {
                    //show error
                }
                IsAlbumsLoading = false;
            });
        }
        public async Task GetArtists()
        {
            IsArtistLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var artists = ApiService.GetInstance().Post<ArtistResult>("/Music/Artist", "{\"page\":1,\"take\":10}");
                    var recentArtistList = new ObservableCollection<Models.Artist>();
                    foreach (var item in artists.items)
                        recentArtistList.Add(item);
                    RecentArtistList = recentArtistList;
                }
                catch (System.Exception ex)
                {
                    //show error
                }
                IsArtistLoading = false;
            });
        }

        public void Receive(CurrentMusicMessageModel message)
        {
            SelectedMusic = message.Music;
        }
    }
}