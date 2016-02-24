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
        public ObservableCollection<Offer> Offers { get; set; }

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

            ClientWindow clientWindow = new ClientWindow(db);

            // tester si la fenetre deleteWindow renvois un vrai
            if (deleteWindow.DialogResult == true)
            {
                Client client = (Client)ListClient.SelectedItem;

                // Si il'y a un client séléctioné.
                if( client != null )
                {

                    // supprimé le client de la base de donnée /!\ ça marche pas
                    db.Client.Remove(client);

                    // supprimé le client de la fenétre.
                    Clients.Remove(client);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
                // Si il n'y a aucun client de séléctioné 
                else
                {
                    // Afficher le message d'erreur 
                    MessageBox.Show("Vous n'avez séléctionné aucun client.");
                }
                
            }

            
        }

        private void ButtonAddOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow(db);

            offerWindow.ShowDialog();
        }

        private void ButtonUpdateOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow(db);

            offerWindow.ShowDialog();

            if (offerWindow.DialogResult == true)
            {
                // ListClient.ItemsSource.UpdateSource();
            }

        }

        private void ButtonDeleteOffer_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow deleteWindow = new DeleteWindow();

            deleteWindow.ShowDialog();

            OfferWindow offerWindow = new OfferWindow(db);

            // tester si la fenetre deleteWindow renvois un vrai
            if (deleteWindow.DialogResult == true)
            {
                Offer offer = (Offer)ListOffer.SelectedItem;

                // Si il'y a un client séléctioné.
                if (offer != null)
                {

                    // supprimé le client de la base de donnée /!\ ça marche pas
                    db.Offer.Remove(offer);

                    // supprimé le client de la fenétre.
                    Offers.Remove(offer);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
                // Si il n'y a aucun client de séléctioné 
                else
                {
                    // Afficher le message d'erreur 
                    MessageBox.Show("Vous n'avez séléctionné aucun offre.");
                }

            }
        }
    }
}
