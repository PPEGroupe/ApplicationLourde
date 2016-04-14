using MegaCasting.DBLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour WebUserWindow.xaml
    /// </summary>
    public partial class WebUserWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db;
        public ObservableCollection<WebUser> WebUsers { get; set; }
        #endregion

        #region Constructeur
        public WebUserWindow(MegaCastingEntities context)
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
            WebUser webUser = (WebUser)this.DataContext;

            if (!String.IsNullOrWhiteSpace(webUser.Account.Password)
                || (String.IsNullOrWhiteSpace(webUser.Account.Password) && !String.IsNullOrWhiteSpace(PasswordTextBox.Password)))
            {
                // Vérifie de ne pas ajouter un partenaire existant
                WebUser webUserExist = db.WebUser.FirstOrDefault(dbWebUser => dbWebUser.Account.Email == EmailTextBox.Text);
                if (webUser.Account.Email == EmailTextBox.Text || webUser.Identifier == 0 && webUserExist == null)
                {
                    FirstnameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    LastnameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    PhoneNumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    AddressTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    CityTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    ZipCodeTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    EmailTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                    if (!String.IsNullOrWhiteSpace(PasswordTextBox.Password))
                    {
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string hash = Crypting.GetMd5Hash(md5Hash, PasswordTextBox.Password);
                            webUser.Account.Password = hash;
                        }
                    }
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Cet email est déjà enregistré");
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir renseigner un mot de passe");
            }
        }
        #endregion
    }
}
