using Microsoft.Maui.Controls;

namespace Aplication
{
    public partial class MainPage : ContentPage
    {
        string userID = "";
        string password = "";
        public MainPage()
        {
            InitializeComponent();
        }
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = entryID.Text;
            Console.WriteLine(oldText);
            Console.WriteLine(newText);
            Console.WriteLine(myText);
        }
        void OnEntryCompletedID(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
            LabRezultat.Text = text;
            userID = text;
        }
        void OnEntryCompletedUsername(object sender, EventArgs e)
        {
            string text = ((Entry)sender).Text;
            LabRezultat.Text = text;
            password = text;
        }
        private void OnCounterClicked(object sender, EventArgs e)
        {
            //preveri
        }
    }

}
