using Microsoft.Maui.Controls;

namespace Aplication
{
    public partial class MainPage : ContentPage
    {
        public static string userID = "";
        public static string password = "";
        public static string username = "";
        public MainPage()
        {
            InitializeComponent();
        }
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = entryID.Text;
        }
        void OnEntryCompletedID(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
            userID = text;
        }
        void OnEntryCompletedUsername(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
            password = text;
        }
        private async void OnCounterClicked(object sender, EventArgs e)
        {
            CheckLogin loginRequest = new CheckLogin();
            bool correctLogin = loginRequest.ReturnLogin(userID, password);

            if (correctLogin)
            {
                username = "User";
                await Navigation.PushAsync(new GPSPage());
            }
            else
            {

            }
        }
    }

}
