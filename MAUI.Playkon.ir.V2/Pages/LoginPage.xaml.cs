using MAUI.Playkon.ir.V2.Data;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;
using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin(object sender, EventArgs e)
        {
            try
            {
                LoginResult result =
                    ApiService.GetInstance().Post<LoginResult>("/Account/Login",
                            "{\"username\":\"" + txtUsername.Text + "\",\"password\":\"" + txtPassword.Text + "\"}");
                if (result.response != null && result.response.status)
                {
                    new AccountData().Add(new Account()
                    {
                        androidVersion = result.apiVersion,
                        code = result.code,
                        email = result.email,
                        apiVersion = result.apiVersion,
                        id = result.id,
                        image = result.image,
                        isRestricted = result.isRestricted,
                        name = result.name,
                        token = result.response.token,
                        username = result.response.username
                    });
                    SecureStorage.SetAsync("isLogged", "true").Wait();
                    SecureStorage.SetAsync("token", result.response.token).Wait();
                    Application.Current.MainPage = new AppShell();
                    Shell.Current.GoToAsync("//Home");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Login faild", "Wrong username or password", "OK");

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void gotoRegisterForm(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RegisterPage();
        }

        private void showPrivacy(object sender, EventArgs e)
        {
            DisplayAlert("Privacy", "حریم خصوصی کاربران\r\n\r\nکاربر گرامی، ما در این نرم افزار حریم خصوصی شما را محترم میشماریم.\r\nدر این خصوص لطفا به موارد زیر توجه بفرمایید:\r\n\r\n1- ما برای اینکه بتوانیم شما را احراز هویت کنیم، به ناچار نیاز به دانستن یکی از موارد زیر داریم:\r\n\r\nپست الکترونیک\r\n\r\nما این مورد را به هیچ عنوان در اختیار هیچ کسی قرار نخواهیم داد.\r\n\r\n2- ما به هیچ کدام از موارد حساس گوشی شما دسترسی نداریم. ما نیاز به دسترسی به میکروفون، دوربین، لیست مخاطبین و ... نداریم.\r\n\r\n3- پلی لیست شما، آهنگ هایی که گوش میکنید و سایر فعالیت های شما در سیستم ما محفوظ هست و در اختیار هیچ کسی قرار نمیگیرید.\r\n\r\n4- استفاده از نرم افزار ما رایگان هست و هیچ گونه هزینه ی برای شما نخواهد داشت.\r\n\r\n5- تمامی اطلاعاتی ارسالی به سرور، توسط پروتکل https و سایر موارد امن شده است و نگرانی باید دزدیده شدن این اطلاعات وجود ندارد.\r\n\r\n6- در صورتی که نرم افزار نیاز به دسترسی برای ذخیره ی آهنگ ها داشته باشد، به شما هشدار داده می شود که این دسترسی صرفا برای ذخیره ی آهنگ ها بر روی دستگاه شماست و شما می توانید این اجازه را به نرم افزار ندهید.", "ok");
        }
    }
}