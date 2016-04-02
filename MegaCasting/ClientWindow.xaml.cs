using MegaCasting.DBLib;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        #region Attribut
        private MegaCastingEntities db;
        #endregion

        #region Constructeur
        public ClientWindow(MegaCastingEntities context)
        {
            db = context;
            InitializeComponent();
        }
        #endregion

        #region Boutons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            // Vérification des champs vides
            if (String.IsNullOrWhiteSpace(this.CompanyTextBox.Text)
                ||String.IsNullOrWhiteSpace(this.WebSiteTextBox.Text)
                ||String.IsNullOrWhiteSpace(this.EmailTextBox.Text)
                ||String.IsNullOrWhiteSpace(this.PhoneNumberTextBox.Text)
                ||String.IsNullOrWhiteSpace(this.FaxTextBox.Text)
                ||String.IsNullOrWhiteSpace(this.AddressTextBox.Text)
                ||String.IsNullOrWhiteSpace(this.ZipCodeTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs avant de valider");
            }
            // Mise à jour de la source
            else
            {
                CompanyTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                WebSiteTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                EmailTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                PhoneNumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                FaxTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                AddressTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                ZipCodeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                this.DialogResult = true;
            }  
        }
        #endregion
    }
}
