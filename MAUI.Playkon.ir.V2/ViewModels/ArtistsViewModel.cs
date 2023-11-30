using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.Services;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class ArtistsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        public ArtistsViewModel()
        {
            ArtistsList = new ObservableCollection<Artist>();
            Task.Run(GetArtists);
        }
        public async Task GetArtists()
        {
            IsBusy = true;

            _ = Task.Run(() =>
            {
                try
                {
                    var artists = ApiService.GetInstance().Post<ArtistResult>("/Music/Artist", "{\"page\":1,\"take\":10}");
                    foreach (var item in artists.items)
                        ArtistsList.Add(item);
                }
                catch (Exception ex)
                {
                }
                IsBusy = false;
            });
        }

        [ObservableProperty]
        private ObservableCollection<Artist> artistsList;
        [ObservableProperty]
        private Artist selectedArtist;

        [RelayCommand]
        private async void GoToPlaylist()
        {
            if (SelectedArtist != null)
            {
                var id = SelectedArtist.id;
                if (id == null)
                    id = SelectedArtist.pArtistId;

                var viewModel = new PlaylistMusicListViewModel(id, PlaylistType.Artist);
                var page = new PlaylistMusicListPage { BindingContext = viewModel };
                await Shell.Current.Navigation.PushAsync(page, true);
            }
        }

        public void Search(string q)
        {
            try
            {
                var result = ApiService.GetInstance().Post<ArtistResult>("/Music/SearchArtists",
                    "{\"q\":\"" + q + "\",\"page\":1,\"take\":50}");

                var list = new ObservableCollection<Artist>();
                foreach (var item in result.items)
                    list.Add(item);

                ArtistsList = list;
            }
            catch (Exception ex)
            {
            }
        }
    }
}