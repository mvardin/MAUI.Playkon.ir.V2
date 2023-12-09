﻿using Android.Provider;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Helper;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.Services;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class HomeViewModel : ObservableObject, IRecipient<MiniPlayerMessage>
    {
        #region Props
        [ObservableProperty]
        private ObservableCollection<MediaItemModel> recentMusicList;
        [ObservableProperty]
        private bool isMusicLoading = true;

        [ObservableProperty]
        private ObservableCollection<Models.Album> recentAlbumList;
        [ObservableProperty]
        private bool isAlbumsLoading = true;

        [ObservableProperty]
        private ObservableCollection<Models.Artist> recentArtistList;
        [ObservableProperty]
        private bool isArtistLoading = true;

        [ObservableProperty]
        private MediaItemModel selectedMusic;
        #endregion

        #region Ctor
        public HomeViewModel()
        {
            StrongReferenceMessenger.Default.Register(this);

            Task.Run(GetMusics);
            Task.Run(GetAlbums);
            Task.Run(GetArtists);
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async void Selection()
        {
            if (SelectedMusic != null)
            {
                StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
                {
                    CurrentMusic = SelectedMusic,
                    QueueLList = RecentMusicList
                });
            }
        }
        #endregion

        #region Methods
        public async Task GetMusics()
        {
            IsMusicLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var songResult = await ApiService.GetInstance().Post<SongResult>("/Music/Recent", "{\"page\":1,\"take\":50}");
                    var recentMusicList = new ObservableCollection<MediaItemModel>();
                    var mediaItemList = MediaManagerConverter.SongListToMediaItemList(songResult.items);
                    foreach (var song in mediaItemList)
                    {
                        recentMusicList.Add(song);
                    }
                    RecentMusicList = recentMusicList;
                }
                catch (System.Exception ex)
                {
                    Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
                }
                IsMusicLoading = false;
            });
        }
        public async Task GetAlbums()
        {
            IsAlbumsLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var albums = await ApiService.GetInstance().Post<AlbumResult>("/Music/Album", "{\"page\":1,\"take\":10}");
                    var recentAlbumList = new ObservableCollection<Models.Album>();
                    foreach (var item in albums.items)
                        recentAlbumList.Add(item);
                    RecentAlbumList = recentAlbumList;
                }
                catch (System.Exception ex)
                {
                    Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
                }
                IsAlbumsLoading = false;
            });
        }
        public async Task GetArtists()
        {
            IsArtistLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    var artists = await ApiService.GetInstance().Post<ArtistResult>("/Music/Artist", "{\"page\":1,\"take\":10}");
                    var recentArtistList = new ObservableCollection<Models.Artist>();
                    foreach (var item in artists.items)
                        recentArtistList.Add(item);
                    RecentArtistList = recentArtistList;
                }
                catch (System.Exception ex)
                {
                    Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
                }
                IsArtistLoading = false;
            });
        }

        #endregion

        #region Reciepiens
        public void Receive(MiniPlayerMessage message)
        {
            //SelectedMusic = message.CurrentMusic;
        }
        #endregion
    }
}