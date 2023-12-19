using CommunityToolkit.Maui.Alerts;
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

    public partial class PlayerViewModel : ObservableObject, IPlayerViewModel, IRecipient<MiniPlayerUIMessage>
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
        public PlayerViewModel()
        {
            StrongReferenceMessenger.Default.Register(this);

            CrossMediaManager.Current.PositionChanged += Current_PositionChanged;
            CrossMediaManager.Current.StateChanged += Current_StateChanged;

            if (CrossMediaManager.Current.Queue != null && CrossMediaManager.Current.Queue.Current != null)
                CurrentMusic = (MediaItemModel)CrossMediaManager.Current.Queue.Current;

            QueueList = new ObservableCollection<MediaItemModel>();
            foreach (var item in CrossMediaManager.Current.Queue)
                QueueList.Add((MediaItemModel)item);

            Duration = CurrentMusic.Duration;
            Maximum = CurrentMusic.Duration.TotalSeconds;
            FavouriteIcon = CurrentMusic.Favourite ? "hearted.png" : "heart.png";
        }

        #endregion

        #region Commands
        [RelayCommand]
        public void Share()
        {
            //TODO Ardin
            //Microsoft.Maui.Share.RequestAsync(
            //    CrossMediaManager.Current.Queue.Current.MediaUri);
        }
        [RelayCommand]
        public void Play()
        {
            CrossMediaManager.Current.PlayPause();
        }
        [RelayCommand]
        public void Mute()
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
        public async void AddToPlaylist()
        {
            var result = ApiService.GetInstance().Get<UserPlaylistResult>("/Playlist/List");
            if (result.items.Any())
            {
                List<string> buttons = new List<string>();
                foreach (var playlist in result.items)
                    buttons.Add(playlist.name);
                var selectedButton = await Shell.Current.DisplayActionSheet("Select Playlist", "Cancel", "OK", buttons.ToArray());

                if (!string.IsNullOrEmpty(selectedButton))
                {
                    var selectedMusic = result.items.Where(a => a.name == selectedButton).FirstOrDefault();
                    if (selectedMusic != null)
                    {
                        var addResult = ApiService.GetInstance().Post<object>("/PlaylistMusic/Add",
                        "{\"musicId\":\"" + CurrentMusic.MusicId + "\",\"playlistId\":\"" + selectedMusic.id + "\"}");
                        Shell.Current.DisplaySnackbar("Added to playlist.");
                    }
                }
            }
            else
            {
                Shell.Current.DisplaySnackbar("You dont have any playlist.");
            }
        }
        [RelayCommand]
        public void ChangeMusic(object obj)
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
                CurrentMusic = nextMusic;
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
            }
        }
        [RelayCommand]
        public void PlaySelectedMusic()
        {
            if (CurrentMusic != null)
            {
                StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
                {
                    CurrentMusic = CurrentMusic
                });
            }
        }
        [RelayCommand]
        public async void MakeFavourite()
        {
            var result = await ApiService.GetInstance().Post<GeneralResult>("/Music/AddOrRemoveMusicFavourite",
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
                Shell.Current.DisplaySnackbar("Faild to add to favourite", null, "OK");
            }
        }
        #endregion

        #region Methods
        private async void checkMusicFavourited()
        {
            try
            {
                var isMusicFavourited = await ApiService.GetInstance().Post<GeneralResult>("/Music/IsMusicFavourited",
                        "{\"id\":\"" + CurrentMusic.MusicId + "\",\"name\":\"string\"}");

                CurrentMusic.Favourite = isMusicFavourited.status;
                FavouriteIcon = CurrentMusic.Favourite ? "hearted.png" : "heart.png";
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
            }
        }

        #endregion

        #region Events
        private void Current_PositionChanged(object? sender, MediaManager.Playback.PositionChangedEventArgs e)
        {
            Position = e.Position;
        }
        private void Current_StateChanged(object? sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            Task.Run(checkMusicFavourited);
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