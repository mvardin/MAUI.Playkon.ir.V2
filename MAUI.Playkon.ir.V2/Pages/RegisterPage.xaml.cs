using MAUI.Playkon.ir.V2.Data;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void btnRegister(object sender, EventArgs e)
        {
            if (txtUsername.Text.StartsWith("09") && txtUsername.Text.Length == "00000000000".Length)
            {
                var result = ApiService.GetInstance().Post<Account>("/Account/Register"
                    , "{\"username\":\"" + txtUsername.Text + "\",\"password\":\"" + txtPassword.Text + "\"}");
                if (!string.IsNullOrEmpty(result.token))
                {
                    new AccountData().Add(new Models.Account()
                    {
                        androidVersion = result.apiVersion,
                        code = result.code,
                        email = result.email,
                        apiVersion = result.apiVersion,
                        id = result.id,
                        image = result.image,
                        isRestricted = result.isRestricted,
                        name = result.name,
                        token = result.token,
                        username = result.username
                    });
                    SecureStorage.SetAsync("isLogged", "true").Wait();
                    SecureStorage.SetAsync("token", result.token).Wait();
                    Application.Current.MainPage = new AppShell();
                    Shell.Current.GoToAsync("///home");
                }
                else
                    DisplayAlert("Register faild", "Please select correct username and password", "OK");
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Register faild", "Please select correct username and password", "OK");
            }
        }

        private void gotoLoginForm(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}