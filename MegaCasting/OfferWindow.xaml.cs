using MegaCasting.DBLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Navigation;
using MegaCasting.GeocodeService;
using MegaCasting.SearchService;
using MegaCasting.ImageryService;
using MegaCasting.RouteService;

namespace MegaCasting
{
    /// <summary>
    /// Logique d'interaction pour OfferWindow.xaml
    /// </summary>
    public partial class OfferWindow : Window
    {
        #region Construction des clones des colonnes
        // Clones de colonnes pour les docké sur Layer 0
        ColumnDefinition column1CloneForLayer0;

        #endregion

        #region Attributs
        private MegaCastingEntities db;
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        private String latitude;
        private String longitude;
        #endregion

        #region Constructeur
        public OfferWindow(MegaCastingEntities context)
        {
            db = context;

            InitializeComponent();
            // Initialise les clones de colonnes pour docker
            column1CloneForLayer0 = new ColumnDefinition();
            column1CloneForLayer0.SharedSizeGroup = "column1";
        }
        #endregion

        #region Calque
        // Basculement entre l'état épinglé et non épinglé du Panel1
        public void Panel1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonPanel1.Visibility == Visibility.Collapsed)
            {
                UndockPanel(1);
                this.MaxWidth = 600;
                this.MinWidth = 600;
                this.Width    = 600;
            }
            else
            {
                this.MaxWidth = 1050;
                this.MinWidth = 1050;
                this.Width    = 1050;
                DockPanel(1);
            }
        }

        // Montre le Panel 1 au survol de la souris sur ce boutton
        public void ButtonPanel1_MouseEnter(object sender, MouseEventArgs e)
        {
            // Affiche le calque 1
            UpdateMap();
            Layer1.Visibility = Visibility.Visible;
        }

        // Met à jour la carte
        private void UpdateMap()
        {
            string address = AddressTextBox.Text;
            string city = CityTextBox.Text;
            string zipCode = ZipCodeTextBox.Text;
            //Get URI and set image
            String imageURI = GetImagery(address + " " + city + " " + zipCode);
            imageResults.Source = new BitmapImage(new Uri(imageURI));
        }

        // Met à jour la carte à la sortie des TextBox : addresse, ville, code postal
        private void UpdateMap_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateMap();
        }


        // Cache le Panel1 Sinon épinglé quand la souris entre en Calque 0
        public void Layer0_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (ButtonPanel1.Visibility == Visibility.Visible)
            {
                Layer1.Visibility = Visibility.Collapsed;
            }
        }

        // Epingle un Panel, qui cache le ButtonPanel correspondant 
        public void DockPanel(int paneNumber)
        {
            if (paneNumber == 1)
            {
                ButtonPanel1.Visibility = Visibility.Collapsed;
                Panel1PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

                // Ajoute la colonne clonée au Layer 0
                // Layer0.ColumnDefinitions.Add(column1CloneForLayer0);
            }
        }

        // Désépingle un Panel, qui cache le ButtonPanel correspondant
        public void UndockPanel(int paneNumber)
        {
            if (paneNumber == 1)
            {
                Layer1.Visibility = Visibility.Collapsed;
                ButtonPanel1.Visibility = Visibility.Visible;
                Panel1PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

                // Retire la colonne clonée au Layer 0
                Layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
            }
        }
        #endregion

        #region Carte
        private GeocodeService.Location GeocodeAddressGetLocation(string address)
        {
            string key = "l4o5yybDpLNG7xrsTmoP~A0DSZhcYTwcaKvEUgBi44g~AsAjqiCU7gZND03eLCfpdqNDGFXfMdzqXYLFVEGnFy1SgQSlDA8ld0BpbCgI4JsD";
            GeocodeRequest geocodeRequest = new GeocodeRequest();

            // Set the credentials using a valid Bing Maps key
            geocodeRequest.Credentials = new GeocodeService.Credentials();
            geocodeRequest.Credentials.ApplicationId = key;

            // Set the full address query
            geocodeRequest.Query = address;

            // Set the options to only return high confidence results 
            ConfidenceFilter[] filters = new ConfidenceFilter[1];
            filters[0] = new ConfidenceFilter();
            filters[0].MinimumConfidence = GeocodeService.Confidence.High;

            // Add the filters to the options
            GeocodeOptions geocodeOptions = new GeocodeOptions();
            geocodeOptions.Filters = filters;
            geocodeRequest.Options = geocodeOptions;

            // Make the geocode request
            GeocodeServiceClient geocodeService = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            GeocodeResponse geocodeResponse;
            try
            {
                geocodeResponse = geocodeService.Geocode(geocodeRequest);
            }
            catch
            {
                MapLabelTextBox.Text = "Adresse non trouvée";
                return null;
            }

            if (geocodeResponse.Results.Length > 0)
            {
                latitude = geocodeResponse.Results[0].Locations[0].Latitude.ToString();
                longitude = geocodeResponse.Results[0].Locations[0].Longitude.ToString();
                MapLabelTextBox.Text = address;
                return geocodeResponse.Results[0].Locations[0];
            }
            else
            {
                MapLabelTextBox.Text = "Adresse non trouvée";
                latitude = null;
                longitude = null;
                return null;
            }

        }

        private string GetImagery(string locationString)
        {
            string key = "l4o5yybDpLNG7xrsTmoP~A0DSZhcYTwcaKvEUgBi44g~AsAjqiCU7gZND03eLCfpdqNDGFXfMdzqXYLFVEGnFy1SgQSlDA8ld0BpbCgI4JsD";
            MapUriRequest mapUriRequest = new MapUriRequest();

            // Set credentials using a valid Bing Maps key
            mapUriRequest.Credentials = new ImageryService.Credentials();
            mapUriRequest.Credentials.ApplicationId = key;

            // Set the location of the requested image
            mapUriRequest.Center = new ImageryService.Location();

            GeocodeService.Location loc = this.GeocodeAddressGetLocation(locationString);

            if (loc != null)
            {
                mapUriRequest.Center.Latitude = loc.Latitude;
                mapUriRequest.Center.Longitude = loc.Longitude;
            }

            // Set the map style and zoom level
            MapUriOptions mapUriOptions = new MapUriOptions();
            mapUriOptions.Style = MapStyle.Road;
            mapUriOptions.ZoomLevel = 15;

            // Set the size of the requested image in pixels
            mapUriOptions.ImageSize = new ImageryService.SizeOfint();
            mapUriOptions.ImageSize.Height = 500;
            mapUriOptions.ImageSize.Width = 500;

            mapUriRequest.Options = mapUriOptions;

            //Make the request and return the URI
            ImageryServiceClient imageryService = new ImageryServiceClient("BasicHttpBinding_IImageryService");
            MapUriResponse mapUriResponse = imageryService.GetMapUri(mapUriRequest);
            return mapUriResponse.Uri;
        }

        #endregion

        #region Boutons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.JobDomainComboBox.Text)
                   || String.IsNullOrWhiteSpace(this.JobComboBox.Text)
                   || String.IsNullOrWhiteSpace(this.ReferenceTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.TitleTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.DateStartPublicationDatePicker.Text)
                   || String.IsNullOrWhiteSpace(this.PublicationDurationTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.DateStartContractDatePicker.Text)
                   || String.IsNullOrWhiteSpace(this.JobQuantityTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.JobDescriptionTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.AddressTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.CityTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.ZipCodeTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs avant de valider");
            }
            else
            {
                // Applique les modifications
                OfferDataContext offerDataContext = (OfferDataContext)this.DataContext;
                offerDataContext.Offer.Latitude = latitude;
                offerDataContext.Offer.Longitude = longitude;
                ReferenceTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                TitleTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DateStartPublicationDatePicker.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
                PublicationDurationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DateStartContractDatePicker.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
                JobQuantityTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                JobDescriptionTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                ProfileDescriptionTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                AddressTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                CityTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                ZipCodeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                this.DialogResult = true;
            }
        }

        #endregion

    }
}
