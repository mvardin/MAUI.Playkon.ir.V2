using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class AccountViewModel : ObservableObject
    {
        private string Password;
        private string password;

        public AccountViewModel()
        {

        }

        [RelayCommand]
        private async Task Login()
        {
            
        }
        [RelayCommand]
        private void Register()
        {

        }
    }
}
