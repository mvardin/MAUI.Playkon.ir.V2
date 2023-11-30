using MAUI.Playkon.ir.V2.ViewModels;

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
            Navigation.RemovePage(this);
            Navigation.PushAsync(new AlbumsPage(), true);
        }
        private void btnMoreArtists(object sender, EventArgs e)
        {
            Navigation.RemovePage(this);
            Navigation.PushAsync(new ArtistsPage(), true);
        }
        private void tappedon_selectedAlbum(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            var albumId = grid.AutomationId;

            PlaylistMusicListViewModel playlistMusicListViewModel = new PlaylistMusicListViewModel(albumId, PlaylistType.Album);

            PlaylistMusicListPage playlistMusicListPage = new PlaylistMusicListPage();
            playlistMusicListPage.BindingContext = playlistMusicListViewModel;

            Shell.Current.Navigation.PushAsync(playlistMusicListPage, true);
        }

        private void tappedon_selectedArtist(object sender, EventArgs e)
        {
            Grid grid = (Grid)sender;
            var artistId = grid.AutomationId;

            PlaylistMusicListViewModel playlistMusicListViewModel = new PlaylistMusicListViewModel(artistId, PlaylistType.Artist);

            PlaylistMusicListPage playlistMusicListPage = new PlaylistMusicListPage();
            playlistMusicListPage.BindingContext = playlistMusicListViewModel;

            Shell.Current.Navigation.PushAsync(playlistMusicListPage, true);

        }

        private void btnMoreMusics(object sender, EventArgs e)
        {

        }
    }
}