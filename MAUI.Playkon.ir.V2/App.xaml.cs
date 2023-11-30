using MAUI.Playkon.ir.V2.Pages;

namespace MAUI.Playkon.ir.V2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool isLogged = Convert.ToBoolean(SecureStorage.GetAsync("isLogged").Result);
            if (!isLogged)
                MainPage = new LoginPage();
            else
                MainPage = new AppShell();
        }
    }
}
