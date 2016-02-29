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
        private MegaCastingEntities db;
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }

        public OfferWindow(MegaCastingEntities context)    
        {
            db = context;

            InitializeComponent();

            // Instancie les listes de classes
            this.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
            this.Jobs       = new ObservableCollection<Job>(db.Job.ToList());

            this.DataContext = this;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.CompanyTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.JobDomainComboBox.Text)
                   || String.IsNullOrWhiteSpace(this.JobComboBox.Text)
                   || String.IsNullOrWhiteSpace(this.ReferenceTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.TitleTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.DateStartPublicationTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.PublicationDurationTextBox.Text)
                   || String.IsNullOrWhiteSpace(this.DateStartContractTextBox.Text)
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
                CompanyTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                ReferenceTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                TitleTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DateStartPublicationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                PublicationDurationTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                DateStartContractTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                JobQuantityTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                JobDescriptionTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                AddressTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                CityTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                ZipCodeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                this.DialogResult = true;
            }
        }
    }
}
