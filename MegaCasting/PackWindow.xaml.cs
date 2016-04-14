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
    /// Logique d'interaction pour PackWindow.xaml
    /// </summary>
    public partial class PackWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db;
        #endregion

        #region Constructeur
        public PackWindow(MegaCastingEntities context)
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
            if (String.IsNullOrWhiteSpace(this.PriceTextBox.Text)
                || String.IsNullOrWhiteSpace(this.NumberOffersTextBox.Text)
                || String.IsNullOrWhiteSpace(this.NumberDaysTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs avant de valider");
            }
            else
            {
                PriceTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                NumberOffersTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                NumberDaysTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                
                this.DialogResult = true;
            }
        }
        #endregion
    }
}
