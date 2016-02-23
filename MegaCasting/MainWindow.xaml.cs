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
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MegaCastingEntities db = new MegaCastingEntities();
        public ObservableCollection<Client> Clients { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            this.Clients = new ObservableCollection<Client>(db.Client.ToList());

            this.DataContext = this;
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow(db);

            Client client = new Client();

            clientWindow.DataContext = client;
            
            clientWindow.ShowDialog();

        }

        private void ButtonUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            
            ClientWindow clientWindow = new ClientWindow(db);

            Client client = (Client)ListClient.SelectedItem;

            clientWindow.DataContext = client;

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

            if (offerWindow.DialogResult == true)
            {
                // ListClient.ItemsSource.UpdateSource();
            }

        }

        private void ButtonDeleteOffer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
