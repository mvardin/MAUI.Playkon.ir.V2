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
    public partial class SearchViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<MediaItemModel> musicList;

        [ObservableProperty]
        private MediaItemModel selectedMusic;

        public SearchViewModel()
        {
        }

        public async void SearchMethod(string q)
        {
            IsBusy = true;
            try
            {
                var result = ApiService.GetInstance().Post<SongResult>("/Music/Search", "{\"q\":\"" + q + "\",\"page\":1,\"take\":50}");

                var list = new ObservableCollection<MediaItemModel>();
                foreach (var item in result.items)
                    list.Add(MediaManagerConverter.SongToMediaItem(item));

                MusicList = list;
            }
            catch (Exception ex)
            {
            }
            IsBusy = false;
        }

        [RelayCommand]
        public void Search(object obj)
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                try
                {
                    string q = obj.ToString();
                    var result = ApiService.GetInstance().Post<SongResult>("/Music/Search", "{\"q\":\"" + q + "\",\"page\":1,\"take\":50}");

                    var list = new ObservableCollection<MediaItemModel>();
                    foreach (var item in result.items)
                        list.Add(MediaManagerConverter.SongToMediaItem(item));

                    MusicList = list;
                }
                catch (Exception ex)
                {
                }
                IsBusy = false;
            });
        }
        [RelayCommand]
        private async Task PlayMusic()
        {
            if (SelectedMusic != null)
            {
                //var viewModel = new PlayerViewModel(SelectedMusic, MusicList);
                //var playerPage = new PlayerPage { BindingContext = viewModel };

                //Shell.Current.Navigation.PushAsync(playerPage, true);

                StrongReferenceMessenger.Default.Send(new CurrentMusicMessageModel()
                {
                    Music = SelectedMusic,
                    PlayNewInstance = true,
                    MusicList = MusicList
                });
            }
        }
    }
}