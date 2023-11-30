using MAUI.Playkon.ir.V2.ViewModels;

namespace MAUI.Playkon.ir.V2.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            this.BindingContext = new ProfileViewModel();
        }

        private void btnAboutus(object sender, EventArgs e)
        {
            DisplayAlert("About us", "With Playkon, you can listen to music and play about millions of songs for free. Stream music you love and find music - or your next favorite song - from all over the world.\r\n\r\n• Discover new music, albums, playlists\r\n• Search for your favorite song, artist\r\n• Enjoy music playlists and an unique daily mix made just for you\r\n• Make and share your own playlists\r\n• Explore the top songs from different genres, places, and decades\r\n• Find music playlists for any mood and activity\r\n• Listen to music and more on your mobile, tablet, desktop, PlayStation, Chromecast, TV, Wear OS watch and speakers\r\n\r\nPlay music for free on your mobile and tablet with Playkon. Download albums, playlists, or just that one song and listen to music offline, wherever you are.\r\n\r\nWith Playkon, you have access to a world of free music, curated playlists, artists you love. Discover new music, top songs or listen to your favorite artists, albums. Create your own music playlists with the latest songs to suit your mood.\r\n\r\nPlaykon makes streaming music easy with curated playlists you can’t find anywhere else. Find music from new artists, stream your favorite album or playlist and listen to music you love for free.\r\n\r\n• Free music made easy – Listen to a playlist, album, or the top songs from any genre.\r\n\r\nListen to music on your tablet for free\r\n• Play any song, artist, album, or playlist and enjoy a personalised music experience with a daily mix to match your taste.", "OK");
        }

        private void btnFAQ(object sender, EventArgs e)
        {
            DisplayAlert("FAQ", "How can i use playkon service?\r\nYou can just sign up to playkon with your mobile number and then login and then play music and enjoy playkon service.\r\n\r\nHow can i create playlist?\r\nAfter login to playkon, you can create playlist of all songs that you want and then play all musics of that playlist and share that playlist with your friends.", "OK");
        }

        private void btnPrivacy(object sender, EventArgs e)
        {
            DisplayAlert("Privacy", "Please read these Terms of Use (these \"Terms\") carefully as they govern your use of (which includes access to) Playkon's personalized services for streaming music and other content, including all of our websites and software applications that incorporate or link to these Terms (collectively, the \"Playkon Service\") and any music, videos, or other material that is made available through the Playkon Service (the \"Content\").\r\n\r\nUse of the Playkon Service may be subject to additional terms and conditions presented by Playkon, which are hereby incorporated by this reference into these Terms.\r\n\r\nBy signing up for, or otherwise using, the Playkon Service, you agree to these Terms. If you do not agree to these Terms, then you must not use the Playkon Service or access any Content.", "OK");
        }

        private void btnLogout(object sender, EventArgs e)
        {
            try
            {
                SecureStorage.RemoveAll();
                var _databasePath = Path.Combine(FileSystem.AppDataDirectory, "Playkon.db3");
                File.Delete(_databasePath);
                Application.Current.MainPage = new LoginPage();
            }
            catch (Exception ex)
            {
            }
        }
    }
}