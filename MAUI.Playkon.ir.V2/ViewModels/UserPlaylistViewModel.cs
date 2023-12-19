using Android.Accounts;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Playkon.ir.V2.Data;
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
            Task.Run(populate);
        }

        private void populate()
        {
            try
            {
                Playlist = new ObservableCollection<UserPlaylist>();
                var result = ApiService.GetInstance().Get<UserPlaylistResult>("/Playlist/List");
                foreach (var item in result.items)
                {
                    Playlist.Add(item);
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
            }
            IsBusy = false;
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
        private async void Edit(object obj)
        {
            try
            {
                UserPlaylist userPlaylist = (UserPlaylist)obj;
                var account = new AccountData().Get();

                string result = await Shell.Current.DisplayPromptAsync(
                    $"Edit {userPlaylist.name}", "Enter new name:", "OK", "Cancel", userPlaylist.name);
                var apiResult = await ApiService.GetInstance().Post<object>(
                                    "/Playlist/Edit",
                                    "{\"pPlayListId\":\"" + userPlaylist.id + "\",\"pUserId\":\"" + account.id +
                                    "\",\"name\":\"" + result + "\"}");
                Shell.Current.DisplaySnackbar("Edit successfully");
                populate();
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error: " + ex.Message);
            }
        }
        [RelayCommand]
        private async void Delete(object obj)
        {
            try
            {
                UserPlaylist userPlaylist = (UserPlaylist)obj;
                bool result = await Shell.Current.DisplayAlert("Delete " + userPlaylist.name, "Are you sure?", "Yes", "No");
                if (result)
                {
                    var apiResult = await ApiService.GetInstance().Post<object>(
                                        "/Playlist/Delete",
                                        "{\"id\":\"" + userPlaylist.id + "\"}");
                    Shell.Current.DisplaySnackbar("Delete successfully");
                    populate();
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error: " + ex.Message);
            }
        }
    }
}
