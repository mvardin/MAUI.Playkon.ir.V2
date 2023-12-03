using MAUI.Playkon.ir.V2.ViewModels;
using MediaManager;

namespace MAUI.Playkon.ir.V2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
        }
        private async Task authenticat()
        {
            await Task.Delay(2000);

            var hasAuth = await SecureStorage.GetAsync("isLogged");

            if (!(hasAuth == null))
            {
                Shell.Current.GoToAsync("///home");
            }
            else
            {
                await Shell.Current.GoToAsync("login");
            }
        }
    }
}