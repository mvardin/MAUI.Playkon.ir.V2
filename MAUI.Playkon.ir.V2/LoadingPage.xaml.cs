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
        await Task.Delay(2000);

        var hasAuth = await SecureStorage.GetAsync("isLogged");

        if (!(hasAuth == null))
        {
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            await Shell.Current.GoToAsync("login");
        }
    }
}