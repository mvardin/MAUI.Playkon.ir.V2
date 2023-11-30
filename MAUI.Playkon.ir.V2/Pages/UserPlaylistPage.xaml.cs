using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPlaylistPage : ContentPage
    {
        public UserPlaylistPage()
        {
            InitializeComponent();
            BindingContext = new UserPlaylistViewModel();
        }
    }
}