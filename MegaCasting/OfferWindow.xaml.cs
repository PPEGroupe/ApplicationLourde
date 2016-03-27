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

        #region Panel
        // Basculement entre l'état épinglé et non épinglé du Panel1
        public void Panel1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonPanel1.Visibility == Visibility.Collapsed)
                UndockPanel(1);
            else
                DockPanel(1);
        }

        // Montre le Panel 1 au survol de la souris sur ce boutton
        public void ButtonPanel1_MouseEnter(object sender, MouseEventArgs e)
        {
            Layer1.Visibility = Visibility.Visible;
        }


        // Cache le Panel1 Sinon épinglé quand la souris entre en Layer 0
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
                Layer0.ColumnDefinitions.Add(column1CloneForLayer0);
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

                // Retire la colonne clonée au Layer 0 et 1
                Layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
            }
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
