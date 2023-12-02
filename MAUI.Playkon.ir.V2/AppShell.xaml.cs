using MAUI.Playkon.ir.V2.Pages;

namespace MAUI.Playkon.ir.V2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Register all routes
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("search", typeof(SearchPage));
            Routing.RegisterRoute("playlist", typeof(UserPlaylistPage));
            Routing.RegisterRoute("profile", typeof(ProfilePage));
            Routing.RegisterRoute("albums", typeof(AlbumsPage));
            Routing.RegisterRoute("artists", typeof(ArtistsPage));
        }
    }
}
