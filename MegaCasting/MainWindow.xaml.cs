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

            try
            {
                // Instancie les listes de classes
                this.Clients = new ObservableCollection<Client>(db.Client.ToList());
                this.Offers = new ObservableCollection<Offer>(db.Offer.ToList());
            }
            catch (Exception)
            {
                // Si SQL Serveur n'est pas lancé, un message est affiché et l'application se ferme
                MessageBox.Show("L'application n'a pas pu démarrer. Veuillez vérifier que SQL Serveur est bien lancé.");
                Application.Current.Shutdown();
            }

            this.DataContext = this;
        }

        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow(db);
            Client client = new Client();

            clientWindow.DataContext = client;
            clientWindow.ShowDialog();

            if (clientWindow.DialogResult == true)
            {
                Clients.Add(client);
                db.Client.Add(client);

                db.SaveChanges();
            }
        }

        private void ButtonUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow(db);
            Client client = (Client)ListClient.SelectedItem;

            clientWindow.DataContext = client;
            clientWindow.ShowDialog();

            if (clientWindow.DialogResult == true)
            {
                db.SaveChanges();
            }
        }

        private void ButtonDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            DeleteWindow deleteWindow = new DeleteWindow();
            deleteWindow.ShowDialog();

            // Teste si la fenetre deleteWindow renvoi un vrai
            if (deleteWindow.DialogResult == true)
            {
                Client client = (Client)ListClient.SelectedItem;

                // S'il y a un client sélectionné
                if( client != null )
                {
                    // Supprime le client de la BDD
                    db.Client.Remove(db.Client.First(dbClient => dbClient.Identifier == client.Identifier));

                    // Supprime le client la liste
                    Clients.Remove(client);

                    // Sauvegarde les changements en BDD
                    db.SaveChanges();
                }
                // S'il aucun client n'est sélectioné 
                else
                {
                    MessageBox.Show("Veuillez sélectionner un client");
                }
            } 
        }

        private void ButtonAddOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow(db);

            Offer offer = new Offer();

            offerWindow.DataContext = offer;

            offerWindow.ShowDialog();

            if (offerWindow.DialogResult == true)
            {

            }
        }

        private void ButtonUpdateOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow(db);
            Offer offer = (Offer)ListClient.SelectedItem;
            
            offerWindow.DataContext = offer;
            offerWindow.ShowDialog();

            if (offerWindow.DialogResult == true)
            {
                db.SaveChanges();
            }
        }

        private void ButtonDeleteOffer_Click(object sender, RoutedEventArgs e)
        {

            DeleteWindow deleteWindow = new DeleteWindow();

            deleteWindow.ShowDialog();

            OfferWindow offerWindow = new OfferWindow(db);

            Offer offer = (Offer)ListOffer.SelectedItem;

            // tester si la fenetre deleteWindow renvois un vrai
            if (deleteWindow.DialogResult == true)
            {
                // Si il'y a une offre séléctionée.

                if (offer != null)
                {
                    // supprimer l'offre de la base de donnée 
                    db.Offer.Remove(db.Offer.First(dbOffer => dbOffer.Identifier == offer.Identifier));

                    // supprimer le l'offre de la fenétre.
                    Offers.Remove(offer);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }// Si il n'y a aucune offre de séléctioné 
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Vous n'avez séléctionné aucune offre.");
            }
        }
    }  
}
