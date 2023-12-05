using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Java.Net;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MediaManager;
using MediaManager.Library;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    internal partial class MiniPlayerViewModel : ObservableObject, IRecipient<CurrentMusicMessageModel>
    {
        [ObservableProperty]
        private MediaItemModel currentMusic;

        [ObservableProperty]
        private string playIcon = "playbutton.png";

        [ObservableProperty]
        private int showMiniPlayer = 0;

        public MiniPlayerViewModel()
        {
            if (CrossMediaManager.Current.Queue.Current != null)
            {
                CurrentMusic = CrossMediaManager.Current.Queue.Current as MediaItemModel;
                if (CrossMediaManager.Current.State == MediaManager.Player.MediaPlayerState.Playing)
                    PlayIcon = "pausebutton.png";
                else PlayIcon = "playbutton.png";

                ShowMiniPlayer = 60;
            }
            StrongReferenceMessenger.Default.Register(this);
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.StateChanged += Current_StateChanged;
        }

        private void Current_StateChanged(object? sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            if (CrossMediaManager.Current.State == MediaManager.Player.MediaPlayerState.Playing)
                PlayIcon = "pausebutton.png";
            else
                PlayIcon = "playbutton.png";
        }

        private void Current_MediaItemChanged(object? sender, MediaManager.Media.MediaItemEventArgs e)
        {
            try
            {
                CurrentMusic = e.MediaItem as MediaItemModel;

                Task.Run(addMusicLog);

                //StrongReferenceMessenger.Default.Send(new CurrentMusicMessageModel()
                //{
                //    Music = CurrentMusic
                //});
            }
            catch (Exception ex)
            {
            }
        }
        private void addMusicLog()
        {
            try
            {
                ApiService.GetInstance().Post<object>("/Setting/AddPlayMusicLog"
                        , "{\"id\":\"" + CurrentMusic.Id + "\",\"name\":\"string\"}");
            }
            catch (Exception ex)
            {
            }
        }


        [RelayCommand]
        private async Task PlayOrPause()
        {
            CrossMediaManager.Current.PlayPause();
        }
        [RelayCommand]
        private void Next()
        {
            var task = CrossMediaManager.Current.PlayNext();
        }
        [RelayCommand]
        private void Previous()
        {
            var task = CrossMediaManager.Current.PlayPrevious();
        }

        public async void Receive(CurrentMusicMessageModel message)
        {
            if (message.Music != null)
                CurrentMusic = message.Music;
            else if (message.MusicList != null)
                CurrentMusic = message.MusicList.FirstOrDefault();

            if (CrossMediaManager.Current.State == MediaManager.Player.MediaPlayerState.Playing)
                PlayIcon = "pausebutton.png";
            else PlayIcon = "playbutton.png";

            ShowMiniPlayer = 60;

            if (message.PlayNewInstance)
            {
                await CrossMediaManager.Current.Play(message.MusicList);
                CrossMediaManager.Current.PlayQueueItem(CurrentMusic);
            }
        }
    }
}
