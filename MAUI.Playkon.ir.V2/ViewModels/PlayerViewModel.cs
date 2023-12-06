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

    public partial class PlayerViewModel : ObservableObject
    {
        #region Props
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

        #region Ctor
        public PlayerViewModel(MediaItemModel music, ObservableCollection<MediaItemModel> list)
        {
            CrossMediaManager.Current.StateChanged += Current_StateChanged;
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.Speed = 1;

            if (music != null)
                CurrentMusic = music;
            else
                CurrentMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Current;

            if (list != null && list.Any())
            {
                QueueList = list;
            }
            else
            {
                QueueList = new ObservableCollection<MediaItemModel>();
                foreach (var item in CrossMediaManager.Current.Queue)
                    QueueList.Add((MediaItemModel)item);
            }
            Duration = CurrentMusic.Duration;
            Maximum = CurrentMusic.Duration.TotalSeconds;
            FavouriteIcon = CurrentMusic.Favourite ? "hearted.png" : "heart.png";
        }
        #endregion

        #region Commands
        [RelayCommand]
        private void Share()
        {
            //TODO Ardin
            //Microsoft.Maui.Share.RequestAsync(
            //    CrossMediaManager.Current.Queue.Current.MediaUri);
        }
        [RelayCommand]
        private void Play()
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
        private void ChangeMusic(object obj)
        {
            try
            {
                MediaItemModel nextMusic = new MediaItemModel();
                if ((string)obj == "P")
                {
                    nextMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Previous;
                }
                else if ((string)obj == "N")
                {
                    nextMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Next;
                }
                StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
                {
                    Music = nextMusic,
                });
                CurrentMusic = nextMusic;
            }
            catch (Exception ex)
            {
            }
        }
        [RelayCommand]
        private void PlaySelectedMusic()
        {
            if (CurrentMusic != null)
            {
                StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
                {
                    Music = CurrentMusic,
                    PlayNewInstance = true,
                });
            }
        }
        [RelayCommand]
        private void MakeFavourite()
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
        #endregion

        #region Methods
        private void checkMusicFavourited()
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

        #region Events
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
        #endregion
    }
}