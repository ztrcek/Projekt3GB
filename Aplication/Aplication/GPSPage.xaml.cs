using Microsoft.Maui.Controls;

namespace Aplication
{
    public partial class GPSPage : ContentPage
    {
        enum Status { doma, dela, malica}
        Status currentStatus = Status.doma;
        public GPSPage()
        {
            InitializeComponent();
            //SetupWelcomeText();
            WorkStatus();
        }
        void WorkStatus()
        {
            switch (currentStatus)
            {
                case Status.doma:
                    Doma();
                    return;
                case Status.dela: 
                    Dela();
                    return;
                case Status.malica:
                    Malica();
                    return;
                default:
                    return;
            }
        }
        void Doma()
        {
            IconImage.Source = "bussinessoluapp.png";
            StatusText.Text = "Si doma";
        }
        void Dela()
        {
            IconImage.Source = "bussinessoluapp.png";
            StatusText.Text = "Delas";
        }
        void Malica()
        {
            IconImage.Source = "bussinessoluapp.png";
            StatusText.Text = "Malica";
        }

        #region GPS
        void SetupWelcomeText()
        {
            LabRezultat.Text = MainPage.username;
            string location = GetCachedLocation().GetAwaiter().GetResult();
        }
        public async Task<string> GetCachedLocation()
        {
            try
            {
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                    return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return "None";
        }
        #endregion
    }
}