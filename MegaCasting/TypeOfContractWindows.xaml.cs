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
    /// Logique d'interaction pour TypeOfContractWindow.xaml
    /// </summary>
    public partial class TypeOfContractWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db;
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        #endregion

        #region Constructeur
        public TypeOfContractWindow(MegaCastingEntities context)
        {
            InitializeComponent();

            // Récupére l'instance de la connexion à la BDD.
            db = context;
        }
        #endregion

        #region Boutons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.LabelTextBox.Text))
            {
                LabelTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez remplir le libellé");
            }
        }
        #endregion
    }
}
