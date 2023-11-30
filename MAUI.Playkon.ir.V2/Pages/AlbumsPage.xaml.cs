using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlbumsPage : ContentPage
    {
        public AlbumsPage()
        {
            InitializeComponent();
            this.BindingContext = new AlbumViewModel();
        }

        private void btnSearch(object sender, System.EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar.Text.Length < 3)
                return;

            AlbumViewModel albumViewModel = new AlbumViewModel();
            albumViewModel.Search(searchBar.Text);
            BindingContext = albumViewModel;
        }
    }
}