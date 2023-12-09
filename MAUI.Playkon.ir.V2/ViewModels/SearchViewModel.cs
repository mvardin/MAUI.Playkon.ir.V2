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
    public partial class SearchViewModel : ObservableObject, IRecipient<MiniPlayerMessage>
    {
        #region Props
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<MediaItemModel> musicList;

        [ObservableProperty]
        private MediaItemModel selectedMusic;
        #endregion

        #region Ctor
        public SearchViewModel()
        {
            StrongReferenceMessenger.Default.Register(this);
        }
        #endregion

        #region Methods
        public void SearchMethod(string q)
        {
            IsBusy = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var result = await ApiService.GetInstance().Post<SongResult>("/Music/Search", "{\"q\":\"" + q + "\",\"page\":1,\"take\":50}");

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
        #endregion

        #region Commands
        [RelayCommand]
        public void Search(object obj)
        {
            IsBusy = true;
            Task.Run(async () =>
            {
                try
                {
                    string q = obj.ToString();
                    var result = await ApiService.GetInstance().Post<SongResult>("/Music/Search", "{\"q\":\"" + q + "\",\"page\":1,\"take\":50}");

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
        private void PlayMusic()
        {
            StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
            {
                CurrentMusic = SelectedMusic,
                QueueLList = MusicList
            });
        }
        #endregion

        public void Receive(MiniPlayerMessage message)
        {
            //SelectedMusic = message.CurrentMusic;
        }
    }
}