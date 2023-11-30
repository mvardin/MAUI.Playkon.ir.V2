using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArtistsPage : ContentPage
    {
        public ArtistsPage()
        {
            InitializeComponent();
            this.BindingContext = new ArtistsViewModel();
        }

        private void btnSearch(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar.Text.Length < 3)
                return;

            ArtistsViewModel artistsViewModel = new ArtistsViewModel();
            artistsViewModel.Search(searchBar.Text);
            BindingContext = artistsViewModel;
        }
    }
}