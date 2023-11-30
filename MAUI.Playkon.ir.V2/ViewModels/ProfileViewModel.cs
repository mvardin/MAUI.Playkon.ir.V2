using CommunityToolkit.Mvvm.ComponentModel;
using MAUI.Playkon.ir.V2.Data;
using MAUI.Playkon.ir.V2.Models;

namespace MAUI.Playkon.ir.V2.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private Account account;
        public ProfileViewModel()
        {
            Account = new AccountData().Get();
        }
    }
}
