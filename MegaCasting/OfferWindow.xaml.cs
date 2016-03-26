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
        // Clones colonnes pour les couches 0 et 1, 2X pour 0 
        ColumnDefinition column1CloneForLayer0;
        ColumnDefinition column2CloneForLayer0;
        ColumnDefinition column2CloneForLayer1;

        public OfferWindow()
        {
            InitializeComponent();
            // Initialize the dummy columns used when docking:
            column1CloneForLayer0 = new ColumnDefinition();
            column1CloneForLayer0.SharedSizeGroup = "column1";
            column2CloneForLayer0 = new ColumnDefinition();
            column2CloneForLayer0.SharedSizeGroup = "column2";
            column2CloneForLayer1 = new ColumnDefinition();
            column2CloneForLayer1.SharedSizeGroup = "column2";
        }
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
        }
        #endregion

        #region Panel
        // Basculement entre l'état épinglé et non épinglé du Panel1
        public void Panel1Pin_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonPanel1.Visibility == Visibility.Collapsed)
                UndockPane(1);
            else
                DockPane(1);
        }

        // Basculement entre l'état épinglé et non épinglé du Panel2
        public void Panel2Pin_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonPanel2.Visibility == Visibility.Collapsed)
                UndockPane(2);
            else
                DockPane(2);
        }

        // Montre le Panel 1 au survol de la souris sur ce boutton
        private void ButtonPanel1_MouseEnter(object sender, MouseEventArgs e)
        {
            Layer1.Visibility = Visibility.Visible;

            // Ajuste pour être sur que le Panel1 est en haut
            ParentGrid.Children.Remove(Layer1);
            //ParentGrid.Children.Add(Layer1);

            // Vérifie que l'autre Panel est caché si il n'est pas épinglé
            if (ButtonPanel2.Visibility == Visibility.Visible)
                Layer2.Visibility = Visibility.Collapsed;
        }

        // Montre le Panel 2 au survol de la souris sur ce boutton
        public void ButtonPanel2_MouseEnter(object sender, RoutedEventArgs e)
        {
            Layer2.Visibility = Visibility.Visible;

            // Ajuste pour être sur que le Panel2 est en haut
            ParentGrid.Children.Remove(Layer2);
            //ParentGrid.Children.Add(Layer2);

            // Vérifie que l'autre Panel est caché si il n'est pas épinglé
            if (ButtonPanel1.Visibility == Visibility.Visible)
                Layer1.Visibility = Visibility.Collapsed;
        }

        // Cache les Panel non épinglés quand la souris entre en Layer 0
        public void Layer0_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (ButtonPanel1.Visibility == Visibility.Visible)
                Layer1.Visibility = Visibility.Collapsed;
            if (ButtonPanel2.Visibility == Visibility.Visible)
                Layer2.Visibility = Visibility.Collapsed;
        }

        // Cache les autres Panel si ils sont non épinglés quand la soiris entre en Panel1
        public void Panel1_MouseEnter(object sender, RoutedEventArgs e)
        {
            // S'assure que l'autre Panel est caché si non épinglé
            if (ButtonPanel2.Visibility == Visibility.Visible)
                Layer2.Visibility = Visibility.Collapsed;
        }

        // Cache les autres Panel si ils sont non épinglés quand la soiris entre en Panel2
        public void Panel2_MouseEnter(object sender, RoutedEventArgs e)
        {
            // S'assure que l'autre Panel est caché si non épinglé
            if (ButtonPanel1.Visibility == Visibility.Visible)
                Layer1.Visibility = Visibility.Collapsed;
        }

        // Epingle un Panel, qui cache le ButtonPanel correspondant 
        public void DockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                ButtonPanel1.Visibility = Visibility.Collapsed;
                Panel1PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

                // Ajoute la colonne clonée au Layer 0
                Layer0.ColumnDefinitions.Add(column1CloneForLayer0);
                // Ajoute le clone de la colonne de Layer1, seulement si le Panel2 est épinglé
                if (ButtonPanel2.Visibility == Visibility.Collapsed) Layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                ButtonPanel2.Visibility = Visibility.Collapsed;
                Panel2PinImage.Source = new BitmapImage(new Uri("pin.gif", UriKind.Relative));

                // Add the cloned column to layer 0:
                Layer0.ColumnDefinitions.Add(column2CloneForLayer0);
                // Add the cloned column to layer 1, but only if pane 1 is docked:
                if (ButtonPanel1.Visibility == Visibility.Collapsed) Layer1.ColumnDefinitions.Add(column2CloneForLayer1);
            }
        }

        // Désépingle un Panel, qui cache le ButtonPanel correspondant
        public void UndockPane(int paneNumber)
        {
            if (paneNumber == 1)
            {
                Layer1.Visibility = Visibility.Collapsed;
                ButtonPanel1.Visibility = Visibility.Visible;
                Panel1PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

                // Retire la colonne clonée au Layer 0 et 1
                Layer0.ColumnDefinitions.Remove(column1CloneForLayer0);
                // Retire, si besoin, les mauvaises colonnes
                Layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
            }
            else if (paneNumber == 2)
            {
                Layer2.Visibility = Visibility.Collapsed;
                ButtonPanel2.Visibility = Visibility.Visible;
                Panel2PinImage.Source = new BitmapImage(new Uri("pinHorizontal.gif", UriKind.Relative));

                // Remove the cloned columns from layers 0 and 1:
                Layer0.ColumnDefinitions.Remove(column2CloneForLayer0);
                // This won't always be present, but Remove silently ignores bad columns:
                Layer1.ColumnDefinitions.Remove(column2CloneForLayer1);
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
