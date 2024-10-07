using Microsoft.Maui.Controls;

namespace Aplication
{
    public partial class GPSPage : ContentPage
    {
        enum Status { doma, dela, malica, potovanje }
        Status currentStatus = Status.doma;
        public GPSPage()
        {
            InitializeComponent();
            //CheckGPS();
            WorkStatus();

            UsernameText.Text = "Welcome back ";
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
                case Status.potovanje:
                    Potovanje();
                    return;
                default:
                    return;
            }
        }
        void Doma()
        {
            StatusText.Text = "Si doma";
        }
        void Dela()
        {
            StatusText.Text = "Delas";
        }
        void Malica()
        {
            StatusText.Text = "Malica";
        }
        void Potovanje()
        {
            StatusText.Text = "Malica";
        }

        private async void Click(object sender, EventArgs e)
        {
            switch (currentStatus)
            {
                case Status.doma:
                    return;
                case Status.dela:
                    await Navigation.PushAsync(new MoreInfo());
                    return;
                case Status.malica:
                    return;
                case Status.potovanje:
                    return;
                default:
                    return;
            }
        }

        #region GPS
        void CheckGPS()
        {
            string location = GetCachedLocation().GetAwaiter().GetResult();
            GpsLocation.Text = location;
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