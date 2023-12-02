using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Models;
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
            StrongReferenceMessenger.Default.Register(this);
            if (CrossMediaManager.Current.IsPlaying())
            {
                StrongReferenceMessenger.Default.Send(
                    new CurrentMusicMessageModel()
                    {
                        IsPlaying = true,
                        Music = CrossMediaManager.Current.Queue.Current as MediaItemModel
                    });
            }
        }

        [RelayCommand]
        private async Task PlayOrPause()
        {
            CrossMediaManager.Current.PlayPause();
            if (CrossMediaManager.Current.State == MediaManager.Player.MediaPlayerState.Playing)
                PlayIcon = "pausebutton.png";
            else
                PlayIcon = "playbutton.png";
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
            CurrentMusic = message.Music;
            if (message.IsPlaying)
                PlayIcon = "pausebutton.png";
            else PlayIcon = "playbutton.png";

            ShowMiniPlayer = 60;

            CrossMediaManager.Current.Play(CurrentMusic);
        }
    }
}
