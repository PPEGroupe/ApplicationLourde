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
    /// Logique d'interaction pour JobDomainWindow.xaml
    /// </summary>
    public partial class JobDomainWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db;
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }

        #endregion

        #region Constructeur
        public JobDomainWindow(MegaCastingEntities context)
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
            // Vérification de la validité des champs
            if (String.IsNullOrWhiteSpace(this.LabelTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs avant de valider");
            }
            else
            {
                LabelTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                this.DialogResult = true;
            }
        }
        #endregion
    }
}
