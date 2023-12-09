using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Playkon.ir.V2.Data;
using MAUI.Playkon.ir.V2.Models;
using MAUI.Playkon.ir.V2.Services;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class AccountViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isLoading;
        [ObservableProperty]
        private string username;
        [ObservableProperty]
        private string password;

        public AccountViewModel()
        {

        }

        [RelayCommand]
        private async Task Login()
        {
            IsLoading = true;
            _ = Task.Run(async () =>
            {
                try
                {
                    LoginResult result = await ApiService.GetInstance().Post<LoginResult>("/Account/Login",
                                    "{\"username\":\"" + Username + "\",\"password\":\"" + Password + "\"}");
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
                        await Shell.Current.GoToAsync("///home");
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Login faild", "Wrong username or password", "OK");

                    }
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Login faild", ex.Message, "OK");
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }
        [RelayCommand]
        private async void Register()
        {
            IsLoading = true;
            _ = Task.Run(async () =>
            {

                try
                {
                    if (Username.StartsWith("09") && Username.Length == "00000000000".Length)
                    {
                        var result = await ApiService.GetInstance().Post<Account>("/Account/Register"
                            , "{\"username\":\"" + Username + "\",\"password\":\"" + Password + "\"}");
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
                            Application.Current.MainPage.DisplayAlert("Register faild", "Please select correct username and password", "OK");
                    }
                    else
                    {
                        Application.Current.MainPage.DisplayAlert("Register faild", "Please select correct username and password", "OK");
                    }
                }
                catch (Exception ex)
                {
                    Application.Current.MainPage.DisplayAlert("Register faild", ex.Message, "OK");
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }
    }
}
