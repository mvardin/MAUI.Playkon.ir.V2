using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MediaManager;

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
                Task.Run(() => checkMusicFavourited(CurrentMusic));

                StrongReferenceMessenger.Default.Send(new CurrentMusicMessageModel()
                {
                    Music = CurrentMusic
                });
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
        private async void checkMusicFavourited(MediaItemModel model)
        {
            try
            {
                var isMusicFavourited = ApiService.GetInstance().Post<GeneralResult>("/Music/IsMusicFavourited",
                        "{\"id\":\"" + CurrentMusic.MusicId + "\",\"name\":\"string\"}");

                model.Favourite = isMusicFavourited.status;
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
            CrossMediaManager.Current.PlayNext();
        }
        [RelayCommand]
        private void Previous()
        {
            CrossMediaManager.Current.PlayPrevious();
        }

        public void Receive(CurrentMusicMessageModel message)
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
                CrossMediaManager.Current.Play(CurrentMusic);
                foreach (var item in message.MusicList)
                    CrossMediaManager.Current.Queue.Add(item);
            }
        }
    }
}
