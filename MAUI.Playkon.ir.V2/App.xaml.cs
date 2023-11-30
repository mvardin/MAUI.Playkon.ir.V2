using MAUI.Playkon.ir.V2.Pages;

namespace MAUI.Playkon.ir.V2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }   
}