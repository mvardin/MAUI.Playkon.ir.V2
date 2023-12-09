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
    internal partial class MiniPlayerViewModel : ObservableObject, IRecipient<MiniPlayerMessage>, IRecipient<MiniPlayerUIMessage>
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
                CurrentMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Current;
                if (CrossMediaManager.Current.State == MediaManager.Player.MediaPlayerState.Playing)
                    PlayIcon = "pausebutton.png";
                else PlayIcon = "playbutton.png";

                ShowMiniPlayer = 60;
            }
            StrongReferenceMessenger.Default.Register<MiniPlayerMessage>(this);
            StrongReferenceMessenger.Default.Register<MiniPlayerUIMessage>(this);
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task PlayOrPause()
        {
            _ = CrossMediaManager.Current.PlayPause();
        }
        [RelayCommand]
        private void Next()
        {
            if (CrossMediaManager.Current.Queue.HasNext)
                StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
                {
                    CurrentMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Next
                });
        }
        [RelayCommand]
        private void Previous()
        {
            if (CrossMediaManager.Current.Queue.HasPrevious)
                StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
                {
                    CurrentMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Previous
                });
        }
        #endregion

        #region Recipients
        public void Receive(MiniPlayerMessage message)
        {
            ShowMiniPlayer = 60;

            if (message.CurrentMusic == null && message.QueueLList != null)
                CurrentMusic = message.QueueLList.FirstOrDefault();
            else
                CurrentMusic = message.CurrentMusic;

            if (message.QueueLList != null && !Tools.CompareList(CrossMediaManager.Current.Queue, message.QueueLList))
            {
                CrossMediaManager.Current.Queue.Clear();
                _ = CrossMediaManager.Current.Play(message.QueueLList);
            }
            _ = CrossMediaManager.Current.PlayQueueItem(CurrentMusic);
        }
        public void Receive(MiniPlayerUIMessage message)
        {
            switch (message.MediaPlayerState)
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
        #endregion
    }
}
