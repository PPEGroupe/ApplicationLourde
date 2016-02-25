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
    /// Logique d'interaction pour OfferWindow.xaml
    /// </summary>
    public partial class OfferWindow : Window
    {
        private MegaCastingEntities db;

        public OfferWindow(MegaCastingEntities context)
            db = context;
            InitializeComponent();          
        {
            db = context;
            InitializeComponent();
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
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
