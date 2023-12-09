using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.Services;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class AlbumViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private Album selectedAlbum;
        [ObservableProperty]
        private ObservableCollection<Album> items;

        public AlbumViewModel()
        {
            Items = new ObservableCollection<Album>();
            Task.Run(GetAlbums);
        }

        public async Task GetAlbums()
        {
            IsBusy = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var albums = await ApiService.GetInstance().Post<AlbumResult>("/Music/Album", "{\"page\":1,\"take\":10}");
                    foreach (var item in albums.items)
                        Items.Add(item);
                }
                catch (Exception ex)
                {
                }
                IsBusy = false;
            });
        }

        [RelayCommand]
        private async void GoToPlaylist()
        {
            if (SelectedAlbum != null)
            {
                var id = SelectedAlbum.id;
                if (id == null)
                    id = SelectedAlbum.pAlbumId;

                var viewModel = new PlaylistMusicListViewModel(id, PlaylistType.Album);
                var page = new PlaylistMusicListPage { BindingContext = viewModel };
                Shell.Current.Navigation.PushAsync(page, true);
            }
        }
        public async void Search(string q)
        {
            try
            {
                var result = await ApiService.GetInstance().Post<AlbumResult>("/Music/SearchAlbums",
                    "{\"q\":\"" + q + "\",\"page\":1,\"take\":50}");

                var list = new ObservableCollection<Album>();
                foreach (var item in result.items)
                    list.Add(item);

                Items = list;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
            }
        }
    }
}
