using MAUI.Playkon.ir.V2.Pages;
using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Views
{
    public partial class MiniPlayerView : ContentView
    {
        public MiniPlayerView()
        {
            InitializeComponent();
            BindingContext = new MiniPlayerViewModel();
        }
        private void btnShowPlayer(object sender, TappedEventArgs e)
        {
            var viewModel = new PlayerViewModel(null, null);
            var playerPage = new PlayerPage { BindingContext = viewModel };
            Navigation.PushModalAsync(playerPage, true);
        }
    }
}