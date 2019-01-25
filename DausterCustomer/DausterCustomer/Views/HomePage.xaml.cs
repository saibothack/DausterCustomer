using DausterCustomer.Models;
using DausterCustomer.ViewModels;
using DurianCode.PlacesSearchBar;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace DausterCustomer.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
        string GooglePlacesApiKey = Constants.googleKey;
        private HomePageViewModel viewModel;
        private List<Pin> lsPins = new List<Pin>();
        private Polyline polylines = new Polyline();

        private static string jsonStyle ="[{'elementType': 'geometry', 'stylers': [{'color': '#1d2c4d'}] }," +
            "{'elementType': 'labels.text.fill','stylers': [{'color': '#8ec3b9'}]}," + 
            "{'elementType': 'labels.text.stroke','stylers': [{'color': '#1a3646'}]}," + 
            "{'featureType': 'administrative.country','elementType': 'geometry.stroke','stylers': [{'color': '#4b6878'}]}," +
            "{'featureType': 'administrative.land_parcel','elementType': 'labels.text.fill','stylers': [{'color': '#64779e'}]}," +
            "{'featureType': 'administrative.province','elementType': 'geometry.stroke','stylers': [{'color': '#4b6878'}]}," +
            "{'featureType': 'landscape.man_made','elementType': 'geometry.stroke','stylers': [{'color': '#334e87'}]}," +
            "{'featureType': 'landscape.natural','elementType': 'geometry','stylers': [{'color': '#023e58'}]}," +
            "{'featureType': 'poi','elementType': 'geometry','stylers': [{'color': '#283d6a'}]}," +
            "{'featureType': 'poi','elementType': 'labels.text.fill','stylers': [{'color': '#6f9ba5'}]}," +
            "{'featureType': 'poi','elementType': 'labels.text.stroke','stylers': [{'color': '#1d2c4d'}]}," +
            "{'featureType': 'poi.park','elementType': 'geometry.fill','stylers': [{'color': '#023e58'}]}," +
            "{'featureType': 'poi.park','elementType': 'labels.text.fill','stylers': [{'color': '#3C7680'}]}," +
            "{'featureType': 'road','elementType': 'geometry','stylers': [{'color': '#304a7d'}]}," +
            "{'featureType': 'road','elementType': 'labels.text.fill','stylers': [{'color': '#98a5be'}]}," +
            "{'featureType': 'road','elementType': 'labels.text.stroke','stylers': [{'color': '#1d2c4d'}]}," +
            "{'featureType': 'road.highway','elementType': 'geometry','stylers': [{'color': '#2c6675'}]}," +
            "{'featureType': 'road.highway','elementType': 'geometry.stroke','stylers': [{'color': '#255763'}]}," +
            "{'featureType': 'road.highway','elementType': 'labels.text.fill','stylers': [{'color': '#b0d5ce'}]}," +
            "{'featureType': 'road.highway','elementType': 'labels.text.stroke','stylers': [{'color': '#023e58'}]}," +
            "{'featureType': 'transit','elementType': 'labels.text.fill','stylers': [{'color': '#98a5be'}]}," +
            "{'featureType': 'transit','elementType': 'labels.text.stroke','stylers': [{'color': '#1d2c4d'}]}," +
            "{'featureType': 'transit.line','elementType': 'geometry.fill','stylers': [{'color': '#283d6a'}]}," +
            "{'featureType': 'transit.station','elementType': 'geometry','stylers': [{'color': '#3a4762'}]}," +
            "{'featureType': 'water','elementType': 'geometry','stylers': [{'color': '#0e1626'}]}," +
            "{'featureType': 'water','elementType': 'labels.text.fill','stylers': [{'color': '#4e6d70'}]}]";

        public HomePage ()
		{
			InitializeComponent();
            map.MapStyle = MapStyle.FromJson(jsonStyle);
            map.UiSettings.ZoomControlsEnabled = false;
            googleSearChBar.BackgroundColor = Color.White;
            BindingContext = viewModel = new HomePageViewModel();
            viewModel.Navigation = this.Navigation;
            LocationEnabled();

        }

        private async void LocationEnabled() {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    await DisplayAlert("Ubicación", "Es necesario utilizar su ubicación", "Aceptar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Location))
                    status = results[Permission.Location];
            }

            if (status == PermissionStatus.Granted)
            {
                var locator = CrossGeolocator.Current;
                //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(1));
                var position = await locator.GetPositionAsync();
                map.MyLocationEnabled = true;
                map.UiSettings.MyLocationButtonEnabled = true;
                searchBar();

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMeters(150)), true);
            }
            else if (status == PermissionStatus.Unknown)
            {
                await DisplayAlert("Ubicación", "Se denego la ubucación, no es posible continuar.", "Aceptar");
            }
        }

        private void searchBar() {
            googleSearChBar.ApiKey = GooglePlacesApiKey;
            googleSearChBar.Type = PlaceType.Address;
            googleSearChBar.Components = new Components("country:mx");
            googleSearChBar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            googleSearChBar.TextChanged += Search_Bar_TextChanged;
            googleSearChBar.MinimumSearchText = 2;
            results_list.ItemSelected += Results_List_ItemSelected;
        }

        private async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction)e.SelectedItem;
            results_list.SelectedItem = null;

            var place = await Places.GetPlace(prediction.Place_ID, GooglePlacesApiKey);

            if (place != null) {
                googleSearChBar.Text = string.Empty;
                results_list.IsVisible = false;
                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(place.Latitude, place.Longitude), Distance.FromMeters(150)), true);
            }
                
        }

        private void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                results_list.IsVisible = false;
                spinner.IsVisible = true;
                spinner.IsRunning = true;
            }
            else
            {
                results_list.IsVisible = false;
                spinner.IsRunning = false;
                spinner.IsVisible = false;
            }
        }

        private void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
                results_list.IsVisible = true;
            else
                results_list.IsVisible = false;
        }

        private void Button_AddMarker(object sender, EventArgs e)
        {
            Position position = new Position(map.VisibleRegion.Center.Latitude,
                map.VisibleRegion.Center.Longitude);

            Pin pin = new Pin() {
                Icon = BitmapDescriptorFactory.FromBundle("marker.png"),
                Type = PinType.Place,
                Label = String.Empty,
                Position = position
            };

            polylines.Positions.Add(position);
            polylines.StrokeColor = Color.White;
            polylines.StrokeWidth = 5f;
            polylines.IsClickable = false;

            lsPins.Add(pin);

            map.Pins.Clear();
            map.Polylines.Clear();
            

            foreach (Pin itm in lsPins) {
                map.Pins.Add(itm);
            }


            if (polylines.Positions.Count > 1)
                map.Polylines.Add(polylines);
        }

        private void Button_Clear(object sender, EventArgs e)
        {
            map.Pins.Clear();
            map.Polylines.Clear();
            lsPins = new List<Pin>();
            polylines = new Polyline();
        }

        private async void Button_Next(object sender, EventArgs e)
        {
            if (lsPins.Count <= 1)
            {
                await DisplayAlert("Ubicación", "Por favor seleccione por lo menos dos ubicaciones a cotizar.", "Aceptar");
            }
            else {
                List<Coordinates> coordinates = new List<Coordinates>();
                for (int i = 0; i < (lsPins.Count - 1); i++)
                {
                    Coordinates coordinate = new Coordinates();
                    coordinate.latitude = lsPins[i].Position.Latitude;
                    coordinate.longitude = lsPins[i].Position.Longitude;
                    coordinates.Add(coordinate);
                }

                acIndicator.IsVisible = true;
                acIndicator.IsRunning = true;
                JObject routes = await App.oServiceManager.getAsyncRouteGoogle(lsPins);
                int iDistance = (int)routes["routes"][0]["legs"][0]["distance"]["value"];
                int iDuration = (int)routes["routes"][0]["legs"][0]["duration"]["value"];
                App.setService.coordinates = coordinates;
                App.setService.meters = iDistance;
                App.setService.time = iDuration;

                QuotationPage quotationPage = new QuotationPage();
                acIndicator.IsVisible = false;
                acIndicator.IsRunning = false;
                await this.Navigation.PushAsync(quotationPage);
            }
        }
    }
}