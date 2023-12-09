using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Playkon.ir.V2.Helper
{
    public class MediaManagerEventHelper
    {
        public void Init()
        {
            CrossMediaManager.Current.MediaItemChanged += Current_MediaItemChanged;
            CrossMediaManager.Current.MediaItemFailed += Current_MediaItemFailed;
            CrossMediaManager.Current.MediaItemFinished += Current_MediaItemFinished;
            CrossMediaManager.Current.StateChanged += Current_StateChanged;
        }

        private void Current_StateChanged(object? sender, MediaManager.Playback.StateChangedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new MiniPlayerUIMessage()
            {
                MediaPlayerState = e.State
            });
        }

        private void Current_MediaItemFinished(object? sender, MediaManager.Media.MediaItemEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
            {
                CurrentMusic = (MediaItemModel)e.MediaItem
            });
        }

        private void Current_MediaItemFailed(object? sender, MediaManager.Media.MediaItemFailedEventArgs e)
        {
            Shell.Current.DisplaySnackbar(e.Message);
        }

        private void Current_MediaItemChanged(object? sender, MediaManager.Media.MediaItemEventArgs e)
        {
            Task.Run(addMusicLog);
        }

        private async void addMusicLog()
        {
            try
            {
                var CurrentMusic = CrossMediaManager.Current.Queue.Current as MediaItemModel;
                _ = ApiService.GetInstance().Post<object>("/Setting/AddPlayMusicLog"
                        , "{\"id\":\"" + CurrentMusic.Id + "\",\"name\":\"string\"}");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
