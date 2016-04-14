using MegaCasting.DBLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            if (String.IsNullOrWhiteSpace(this.CompanyTextBox.Text) || String.IsNullOrWhiteSpace(this.EmailTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs obligatoire avant de valider");
            }
            // Mise à jour de la source
            else
            {
                Client client = (Client)this.DataContext;

                if (!String.IsNullOrWhiteSpace(client.Account.Password)
                    || (String.IsNullOrWhiteSpace(client.Account.Password) && !String.IsNullOrWhiteSpace(PasswordTextBox.Password)))
                {
                    // Vérifie de ne pas ajouter un client existant
                    if (client.Account.Email == EmailTextBox.Text || client.Identifier == 0 && db.Client.FirstOrDefault(dbClient => dbClient.Account.Email == EmailTextBox.Text) == null)
                    {
                        if (client.Company == CompanyTextBox.Text || db.Client.FirstOrDefault(dbClient => dbClient.Company == CompanyTextBox.Text) == null)
                        {
                            CompanyTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            WebSiteTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            EmailTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            PhoneNumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            FaxTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            AddressTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            CityTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            ZipCodeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            IsValidCheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();

                            if (!String.IsNullOrWhiteSpace(PasswordTextBox.Password))
                            {
                                using (MD5 md5Hash = MD5.Create())
                                {
                                    string hash = Crypting.GetMd5Hash(md5Hash, PasswordTextBox.Password);
                                    client.Account.Password = hash;
                                }
                            }
                            this.DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Cet société est déjà enregistrée");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cet email est déjà enregistré");
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez renseigner un mot de passe");
                }
            }  
        }
        #endregion
    }
}
