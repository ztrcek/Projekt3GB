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
            HomeIcon.Source = "iconimagehome.png";
            StatusText.Text = "Si doma";
        }
        void Dela()
        {
            HomeIcon.Source = "iconimagework.png";
            StatusText.Text = "Delas";
        }
        void Malica()
        {
            HomeIcon.Source = "iconimagelunchbrake.png";
            StatusText.Text = "Malica";
        }
        void Potovanje()
        {
            HomeIcon.Source = "iconimagetrip.png";
            StatusText.Text = "Malica";
        }

        private async void Click(object sender, EventArgs e)
        {
            switch (currentStatus)
            {
                case Status.doma:
                    await Navigation.PushAsync(new MoreInfo());
                    return;
                case Status.dela:
                    await Navigation.PushAsync(new MoreInfo());
                    return;
                case Status.malica:
                    await Navigation.PushAsync(new MoreInfo());
                    return;
                case Status.potovanje:
                    await Navigation.PushAsync(new MoreInfo());
                    return;
                default:
                    return;
            }
        }

        public void RefreshStatus(object sender, EventArgs e)
        {
            if(currentStatus == Status.doma)
            {
                currentStatus = Status.dela;
            }

            WorkStatus();
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