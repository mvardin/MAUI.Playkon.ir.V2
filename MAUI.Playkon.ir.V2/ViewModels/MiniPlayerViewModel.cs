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
    internal partial class MiniPlayerViewModel : ObservableObject, IRecipient<MiniPlayerMessage>
    {
        #region Props
        [ObservableProperty]
        private MediaItemModel currentMusic;

        [ObservableProperty]
        private string playIcon = "playbutton.png";

        [ObservableProperty]
        private int showMiniPlayer = 0;
        #endregion

        #region Ctore
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
        #endregion

        #region Events
        private void Current_StateChanged(object? sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            switch (CrossMediaManager.Current.State)
            {
                case MediaManager.Player.MediaPlayerState.Stopped:
                    PlayIcon = "playbutton.png";
                    break;
                case MediaManager.Player.MediaPlayerState.Loading:
                    PlayIcon = "loading2.png";
                    break;
                case MediaManager.Player.MediaPlayerState.Buffering:
                    PlayIcon = "loading2.png";
                    break;
                case MediaManager.Player.MediaPlayerState.Playing:
                    PlayIcon = "pausebutton.png";
                    break;
                case MediaManager.Player.MediaPlayerState.Paused:
                    PlayIcon = "playbutton.png";
                    break;
                case MediaManager.Player.MediaPlayerState.Failed:
                    PlayIcon = "offbutton.png";
                    break;
                default:
                    break;
            }
        }

        private void Current_MediaItemChanged(object? sender, MediaManager.Media.MediaItemEventArgs e)
        {
            try
            {
                Task.Run(addMusicLog);
                CurrentMusic = (MediaItemModel)e.MediaItem;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Methods
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
        #endregion

        #region Commands
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
        #endregion

        #region Recipients
        public async void Receive(MiniPlayerMessage message)
        {
            ShowMiniPlayer = 60;

            if (message.PlayNewInstance)
            {
                CrossMediaManager.Current.Play(message.MusicList);
                CrossMediaManager.Current.PlayQueueItem(message.Music);
            }
        }
        #endregion
    }
}
