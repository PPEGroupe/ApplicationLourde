using MegaCasting.DBLib;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace MegaCasting
{
    /// <summary>
    /// Logique d'interaction pour DeleteWindow.xaml
    /// </summary>
    public partial class DeleteWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db = new MegaCastingEntities();
        public string Description { get; set; }
        #endregion

        #region Constructeur
        public DeleteWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        #endregion

        #region Boutons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            // return false.
            this.DialogResult = false;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            // return true.
            this.DialogResult = true;
        }
        #endregion
    }
}
