using CommunityToolkit.Mvvm.Messaging;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.ViewModels;
using MediaManager;
using System.Collections.ObjectModel;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();
        }

        private void btnMoreAlbums(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("albums", true);
        }
        private void btnMoreArtists(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("artists", true);
        }
        private void tappedon_selectedAlbum(object sender, TappedEventArgs e)
        {
            Grid grid = (Grid)sender;
            var musicId = Guid.Parse(grid.AutomationId);

            HomeViewModel homeViewModel = (HomeViewModel)BindingContext;

            var SelectedMusic = homeViewModel.RecentFeaturedList.Where(a => a.MusicId == musicId).FirstOrDefault();

            StrongReferenceMessenger.Default.Send(new MiniPlayerMessage()
            {
                CurrentMusic = SelectedMusic,
                QueueLList = homeViewModel.RecentFeaturedList
            });
        }
        private void tappedon_selectedPlaylist(object sender, TappedEventArgs e)
        {
            Grid grid = (Grid)sender;
            var playlistId = grid.AutomationId;

            PlaylistMusicListViewModel playlistMusicListViewModel = new PlaylistMusicListViewModel(playlistId, PlaylistType.Playlist);

            PlaylistMusicListPage playlistMusicListPage = new PlaylistMusicListPage();
            playlistMusicListPage.BindingContext = playlistMusicListViewModel;

            Shell.Current.Navigation.PushAsync(playlistMusicListPage, true);

        }
    }
}