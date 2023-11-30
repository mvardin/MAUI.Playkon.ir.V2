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
        }
    }
}
