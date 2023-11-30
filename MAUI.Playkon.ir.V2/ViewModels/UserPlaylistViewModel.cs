using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.Services;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class UserPlaylistViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private UserPlaylist selectedPlaylist;

        [ObservableProperty]
        private ObservableCollection<UserPlaylist> playlist;

        public UserPlaylistViewModel()
        {
            IsBusy = true;
            Task.Run(() =>
            {
                try
                {
                    Playlist = new ObservableCollection<UserPlaylist>();
                    var result = ApiService.GetInstance().Get<UserPlaylistResult>("/Playlist/List");
                    foreach (var item in result.items)
                    {
                        playlist.Add(item);
                    }
                }
                catch (Exception ex)
                {
                }
                IsBusy = false;
            });
        }

        [RelayCommand]
        private void PlayPlaylist()
        {
            if (SelectedPlaylist != null)
            {
                var viewModel = new PlaylistMusicListViewModel(SelectedPlaylist.id, PlaylistType.Playlist);
                var page = new PlaylistMusicListPage { BindingContext = viewModel };
                Shell.Current.Navigation.PushAsync(page, true);
            }
        }
        [RelayCommand]
        private void Edit(object obj)
        {
        }
        [RelayCommand]
        private void Delete(object obj)
        {
        }
    }
}
