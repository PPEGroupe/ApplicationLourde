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
        public ObservableCollection<Job> Jobs { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Instancie les listes de classes
                this.Clients = new ObservableCollection<Client>(db.Client.ToList());
                this.Offers  = new ObservableCollection<Offer>(db.Offer.ToList());
                this.Jobs  = new ObservableCollection<Job>(db.Job.ToList());
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

            if (client != null)
            {
                clientWindow.DataContext = client;

                clientWindow.ShowDialog();

                if (clientWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez séléctionné aucun client.");
            }
        }

        private void ButtonDeleteClient_Click(object sender, RoutedEventArgs e)
        {

            Client client = (Client)ListClient.SelectedItem;

            // Teste si la fenetre deleteWindow renvoi un vrai
            if (client != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();

                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {
                    // Si il'y a une offre séléctionée.             
                    // supprimer le client de la base de donnée 
                    db.Client.Remove(db.Client.First(dbClient => dbClient.Identifier == client.Identifier));

                    // supprimer le l'offre de la fenétre.
                    Clients.Remove(client);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Vous n'avez séléctionné aucune offre.");
            }
        }

        private void ButtonAddOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow(db);
            Offer offer = new Offer();
            OfferDataContext offerDataContext = new OfferDataContext();
            offerDataContext.Offer = offer;
            offerDataContext.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
            offerDataContext.Jobs = new ObservableCollection<Job>(db.Job.ToList());
            offerWindow.ShowDialog();

            if (offerWindow.DialogResult == true)
            {
                Offers.Add(offer);
                db.Offer.Add(offer);

                db.SaveChanges();
                
            }
        }

        private void ButtonUpdateOffer_Click(object sender, RoutedEventArgs e)
        {
            OfferWindow offerWindow = new OfferWindow(db);
            Offer offer = (Offer)ListOffer.SelectedItem;
            OfferDataContext offerDataContext = new OfferDataContext();
            offerDataContext.Offer = offer;
            offerDataContext.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
            offerDataContext.Jobs = new ObservableCollection<Job>(db.Job.ToList());

            if (offer != null)
            {
                offerWindow.DataContext = offerDataContext;
                offerWindow.ShowDialog();

                if (offerWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez séléctionné aucune offre.");
            }
        }

        private void ButtonDeleteOffer_Click(object sender, RoutedEventArgs e)
        {
            Offer offer = (Offer)ListOffer.SelectedItem;

            if (offer != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();

                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {
                    // Si il'y a une offre séléctionée.             
                    // supprimer l'offre de la base de donnée 
                    db.Offer.Remove(db.Offer.First(dbOffer => dbOffer.Identifier == offer.Identifier));

                    // supprimer le l'offre de la fenétre.
                    Offers.Remove(offer);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Vous n'avez séléctionné aucune offre.");
            }
        }
    }  
}
