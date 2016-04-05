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
    /// Logique d'interaction pour PartnerWindow.xaml
    /// </summary>
    public partial class PartnerWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db;
        public ObservableCollection<Partner> Partners { get; set; }
        #endregion

        #region Constructeur
        public PartnerWindow(MegaCastingEntities context)
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
            if (String.IsNullOrWhiteSpace(this.URLTextBox.Text)
                || String.IsNullOrWhiteSpace(this.EmailTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs avant de valider");
            }
            else
            {
                Partner partner = (Partner)this.DataContext;

                if (!String.IsNullOrWhiteSpace(partner.Password)
                    || (String.IsNullOrWhiteSpace(partner.Password) && !String.IsNullOrWhiteSpace(PasswordTextBox.Password)))
                {
                    // Vérifie de ne pas ajouter un partenaire existant
                    if (partner.Email == EmailTextBox.Text || partner.Identifier == 0 && db.Partner.FirstOrDefault(dbPartner => dbPartner.Email == EmailTextBox.Text) == null)
                    {
                        if (partner.URL == URLTextBox.Text || db.Partner.FirstOrDefault(dbPartner => dbPartner.URL == URLTextBox.Text) == null)
                        {
                            URLTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                            EmailTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                            if (!String.IsNullOrWhiteSpace(PasswordTextBox.Password))
                            {
                                using (MD5 md5Hash = MD5.Create())
                                {
                                    string hash = Crypting.GetMd5Hash(md5Hash, PasswordTextBox.Password);
                                    partner.Password = hash;
                                }
                            }
                            this.DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Cet URL est déjà enregistré");
                        }
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
        }
        #endregion
    }
}
