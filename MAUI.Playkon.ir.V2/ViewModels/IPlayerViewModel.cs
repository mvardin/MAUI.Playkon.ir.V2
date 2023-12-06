using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MediaManager;
using SkiaSharp;
using System;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public interface IPlayerViewModel
    {
        public MediaItemModel CurrentMusic { get; set; }
        public ObservableCollection<MediaItemModel> QueueList { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan Position { get; set; }
        public double Maximum { get; set; }
        public string PlayIcon { get; set; }
        public string MuteIcon { get; set; }
        public string FavouriteIcon { get; set; }

        public void Share();
        public void Play();
        public void Mute();
        public void ChangeMusic(object obj);
        public void PlaySelectedMusic();
    }
}
