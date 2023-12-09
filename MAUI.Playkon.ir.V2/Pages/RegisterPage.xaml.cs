using MAUI.Playkon.ir.V2.Data;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new AccountViewModel();
        }

        private void gotoLoginForm(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}