using Android.Accounts;
using CommunityToolkit.Maui.Alerts;
using MAUI.Playkon.ir.V2.Data;

namespace MAUI.Playkon.ir.V2;

public partial class LoadingPage : ContentPage
{
    public LoadingPage()
    {
        InitializeComponent();
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _ = authenticat();
        base.OnNavigatedTo(args);
    }
    private async Task authenticat()
    {
        try
        {
            await Task.Delay(2000);

            var account = new AccountData().Get();

            if (account != null && !string.IsNullOrEmpty(account.token))
            {
                await Shell.Current.GoToAsync("//home");
            }
            else
            {
                await Shell.Current.GoToAsync("login");
            }
        }
        catch (Exception ex)
        {
            Shell.Current.DisplaySnackbar("Error:" + ex.Message, null, "OK");
        }
    }
}