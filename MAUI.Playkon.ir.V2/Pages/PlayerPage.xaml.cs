﻿namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            InitializeComponent();
        }

        private void btnBack(object sender, EventArgs e)
        {
            try
            {
                Shell.Current.Navigation.PopModalAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}