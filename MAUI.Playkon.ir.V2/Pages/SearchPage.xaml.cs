using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = new SearchViewModel();
        }
        private void btnSearch(object sender, EventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (searchBar.Text.Length < 3)
                return;

            SearchViewModel searchViewModel = new SearchViewModel();
            searchViewModel.SearchMethod(searchBar.Text);
            BindingContext = searchViewModel;
        }
    }
}