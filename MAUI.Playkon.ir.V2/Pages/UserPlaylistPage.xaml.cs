using CommunityToolkit.Maui.Alerts;
using MAUI.Playkon.ir.V2.Services;
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

        private async void btnAddNewPlaylist(object sender, EventArgs e)
        {
            try
            {
                var result = await Shell.Current.DisplayPromptAsync("Add new playlist", "Enter name of new playlist", "OK", "Cancel", "My new playlist");
                if (!string.IsNullOrEmpty(result))
                {
                    var addResult = await ApiService.GetInstance().Post<object>("/Playlist/Edit", "{\"name\":\"" + result + "\"}");
                    Shell.Current.DisplaySnackbar("Playlist added.");
                    UserPlaylistViewModel userPlaylistViewModel = (UserPlaylistViewModel)BindingContext;
                    userPlaylistViewModel.populate();
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplaySnackbar("Error: " + ex.Message);
            }
        }
    }
}