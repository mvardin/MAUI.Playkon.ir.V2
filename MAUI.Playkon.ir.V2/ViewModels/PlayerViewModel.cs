using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MediaManager;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{

    public partial class PlayerViewModel : ObservableObject, IRecipient<CurrentMusicMessageModel>
    {
        #region Observable
        [ObservableProperty]
        private MediaItemModel currentMusic;

        [ObservableProperty]
        private ObservableCollection<MediaItemModel> queueList;

        [ObservableProperty]
        private TimeSpan duration;

        [ObservableProperty]
        private TimeSpan position;

        [ObservableProperty]
        double maximum = 100f;

        [ObservableProperty]
        private string playIcon = "playbutton.png";

        [ObservableProperty]
        private string muteIcon = "sound.png";

        [ObservableProperty]
        private string favouriteIcon = "heart.png";

        #endregion

        public PlayerViewModel(MediaItemModel music, ObservableCollection<MediaItemModel> list)
        {
            StrongReferenceMessenger.Default.Register(this);

            CrossMediaManager.Current.StateChanged += Current_StateChanged;
            //CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.Speed = 1;

            if (music != null)
                CurrentMusic = music;
            else
                CurrentMusic = CrossMediaManager.Current.Queue.Current as MediaItemModel;

            if (list != null && list.Any())
            {
                QueueList = list;
            }
            else
            {
                QueueList = new ObservableCollection<MediaItemModel>();
                foreach (var item in CrossMediaManager.Current.Queue)
                    QueueList.Add(item as MediaItemModel);
            }
            Duration = CurrentMusic.Duration;
            Maximum = CurrentMusic.Duration.TotalSeconds;
            FavouriteIcon = CurrentMusic.Favourite ? "hearted.png" : "heart.png";
        }

        #region Command
        [RelayCommand]
        private void Share()
        {
            //TODO Ardin
            //Microsoft.Maui.Share.RequestAsync(
            //    CrossMediaManager.Current.Queue.Current.MediaUri);
        }
        [RelayCommand]
        private async Task Play()
        {
            CrossMediaManager.Current.PlayPause();
        }
        [RelayCommand]
        private void Mute()
        {
            if (CrossMediaManager.Current.Volume != null)
                if (CrossMediaManager.Current.Volume.Muted)
                {
                    CrossMediaManager.Current.Volume.Muted = false;
                    MuteIcon = "sound.png";
                }
                else
                {
                    CrossMediaManager.Current.Volume.Muted = true;
                    MuteIcon = "soundoff.png";
                }
        }
        [RelayCommand]
        private async void ChangeMusic(object obj)
        {
            try
            {
                if ((string)obj == "P")
                {
                    var task = CrossMediaManager.Current.PlayPrevious();
                    _ = task.ContinueWith((task) =>
                    {
                        StrongReferenceMessenger.Default.Send(new CurrentMusicMessageModel()
                        {
                            Music = CurrentMusic,
                        });
                    });
                }
                else if ((string)obj == "N")
                {
                    var task = CrossMediaManager.Current.PlayNext();
                    _ = task.ContinueWith((task) =>
                    {
                        StrongReferenceMessenger.Default.Send(new CurrentMusicMessageModel()
                        {
                            Music = CurrentMusic,
                        });
                    });
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void Current_MediaItemChanged(object? sender, MediaManager.Media.MediaItemEventArgs e)
        {
            try
            {
                Task.Run(checkMusicFavourited);
            }
            catch (Exception ex)
            {
            }
        }
        private void Current_PositionChanged(object? sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Position = e.Position;
        }
        private void Current_StateChanged(object? sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            if (CrossMediaManager.Current.State == MediaManager.Player.MediaPlayerState.Playing)
                PlayIcon = "pausebutton.png";
            else
                PlayIcon = "playbutton.png";
        }
        private void Current_MediaItemFailed(object? sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
        }

        [RelayCommand]
        private async Task PlaySelectedMusic()
        {
            if (CurrentMusic != null)
            {
                await CrossMediaManager.Current.Play(QueueList);
                CrossMediaManager.Current.PlayQueueItem(CurrentMusic);
            }
        }

        [RelayCommand]
        private async void MakeFavourite()
        {
            var result = ApiService.GetInstance().Post<GeneralResult>("/Music/AddOrRemoveMusicFavourite",
                "{\"id\":\"" + CurrentMusic.MusicId + "\",\"name\":\"string\"}");
            if (result.status)
            {
                if (FavouriteIcon == "heart.png")
                    FavouriteIcon = "hearted.png";
                else
                    FavouriteIcon = "heart.png";
            }
            else
            {
                Shell.Current.DisplayAlert("Faild to add to favourite", "Faild to add to favourite", "OK");
            }
        }
        private async void checkMusicFavourited()
        {
            try
            {
                var isMusicFavourited = ApiService.GetInstance().Post<GeneralResult>("/Music/IsMusicFavourited",
                        "{\"id\":\"" + CurrentMusic.MusicId + "\",\"name\":\"string\"}");

                CurrentMusic.Favourite = isMusicFavourited.status;
                FavouriteIcon = CurrentMusic.Favourite ? "hearted.png" : "heart.png";
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region Methods
        public void Receive(CurrentMusicMessageModel message)
        {
            CurrentMusic = message.Music;
        }
        #endregion
    }
}