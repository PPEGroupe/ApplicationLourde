using MegaCasting.DBLib;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MegaCasting
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MegaCastingEntities db = new MegaCastingEntities();

        public MainWindow()
        {
            InitializeComponent();

            List clients = new List();

            Client client = new Client();

        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();

            clientWindow.ShowDialog();
        }

        private void ButtonUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();

            clientWindow.ShowDialog();
        }

        private void ButtonDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow deleteWindow = new DeleteWindow();

            deleteWindow.ShowDialog();
        }

        private void ButtonAddOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow();

            offerWindow.ShowDialog();
        }

        private void ButtonUpdateOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow();

            offerWindow.ShowDialog();
        }

        private void ButtonDeleteOffer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
