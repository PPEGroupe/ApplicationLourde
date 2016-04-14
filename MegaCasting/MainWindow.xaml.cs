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
        #region Attributs
        private MegaCastingEntities db = new MegaCastingEntities();
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Partner> Partners { get; set; }
        public ObservableCollection<WebUser> WebUsers { get; set; }
        public ObservableCollection<TypeOfContract> TypeOfContracts { get; set; }
        public ObservableCollection<Pack> Packs { get; set; }
        #endregion

        #region Constructeur
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                // Instancie les listes de classes
                this.Clients = new ObservableCollection<Client>(db.Client.ToList());
                this.Offers = new ObservableCollection<Offer>(db.Offer.ToList());
                this.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
                this.Partners = new ObservableCollection<Partner>(db.Partner.ToList());
                this.WebUsers = new ObservableCollection<WebUser>(db.WebUser.ToList());
                this.TypeOfContracts = new ObservableCollection<TypeOfContract>(db.TypeOfContract.ToList());
                this.Packs = new ObservableCollection<Pack>(db.Pack.ToList());

                this.Jobs = new ObservableCollection<Job>();
            }
            catch (Exception)
            {
                // Si SQL Serveur n'est pas lancé, un message est affiché et l'application se ferme
                MessageBox.Show("L'application n'a pas pu démarrer. Veuillez vérifier que SQL Serveur est bien lancé.");
                Application.Current.Shutdown();
            }

            this.DataContext = this;
        }
        #endregion

        #region Boutons Clients
        private void ButtonAddClient_Click(object sender, RoutedEventArgs e)
        {
            Client client = new Client();
            ClientWindow clientWindow = new ClientWindow(db);

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
            Client client = (Client)ListClient.SelectedItem;

            if (client != null)
            {
                ClientWindow clientWindow = new ClientWindow(db);
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
                if (db.Offer.FirstOrDefault(dbOffer => dbOffer.IdClient == client.Identifier) != null)
                {
                    MessageBox.Show("Impossible de supprimer ce client car il a créé une ou plusieurs offre(s).");
                }
                else
                {
                    DeleteWindow deleteWindow = new DeleteWindow();
                    deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer le client " + client.Company + " ?";
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
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un client.");
            }
        }
        #endregion

        #region Boutons Offre
        private void ButtonAddOffer_Click(object sender, RoutedEventArgs e)
        {
            Offer offer = new Offer();
            OfferWindow offerWindow = new OfferWindow(db);
            offer.DateStartContract = DateTime.Today;
            offer.DateStartPublication = DateTime.Today;
            OfferDataContext offerDataContext = new OfferDataContext();
            offerDataContext.Offer = offer;
            offerDataContext.Clients = new ObservableCollection<Client>(db.Client.ToList());
            offerDataContext.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
            offerDataContext.Jobs = new ObservableCollection<Job>(db.Job.ToList());
            offerDataContext.TypeOfContracts = new ObservableCollection<TypeOfContract>(db.TypeOfContract.ToList());

            offerWindow.DataContext = offerDataContext;
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
            Offer offer = (Offer)ListOffer.SelectedItem;

            if (offer != null)
            {
                OfferWindow offerWindow = new OfferWindow(db);
                OfferDataContext offerDataContext = new OfferDataContext();
                offerDataContext.Offer = offer;
                offerDataContext.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
                offerDataContext.Jobs = new ObservableCollection<Job>(db.Job.ToList());
                offerDataContext.TypeOfContracts = new ObservableCollection<TypeOfContract>(db.TypeOfContract.ToList());
                offerDataContext.Clients = new ObservableCollection<Client>(db.Client.ToList());


                offerWindow.DataContext = offerDataContext;
                offerWindow.ShowDialog();

                if (offerWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une offre.");
            }
        }

        private void ButtonDeleteOffer_Click(object sender, RoutedEventArgs e)
        {
            Offer offer = (Offer)ListOffer.SelectedItem;

            if (offer != null)
            {
                DateTime now = DateTime.Now;
                TimeSpan gap = now - offer.DateStartPublication;
                // Vérifie si l'offre est dans la période de publication
                if (gap.Days <= offer.PublicationDuration)
                {
                    MessageBox.Show("Cette offre est en cours de publication.");
                }
                else
                {
                    DeleteWindow deleteWindow = new DeleteWindow();
                    deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer l'offre n°" + offer.Reference + " ?";
                    deleteWindow.ShowDialog();
                    
                    if (deleteWindow.DialogResult == true)
                    {          
                        // Supprime l'offre de la base de donnée 
                        db.Offer.Remove(db.Offer.First(dbOffer => dbOffer.IdClient == offer.IdClient));

                        // Supprime le l'offre de la fenétre.
                        Offers.Remove(offer);

                        // Sauvegarder les changements.
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner une offre.");
            }
        }
        #endregion

        #region Boutons Domaine de Métier
        private void ButtonAddJobDomain_Click(object sender, RoutedEventArgs e)
        {
            JobDomain jobDomain = new JobDomain();
            JobDomainWindow jobDomainWindow = new JobDomainWindow(db);

            jobDomainWindow.DataContext = jobDomain;
            jobDomainWindow.ShowDialog();

            if (jobDomainWindow.DialogResult == true)
            {
                JobDomains.Add(jobDomain);
                db.JobDomain.Add(jobDomain);

                db.SaveChanges();
            }
        }

        private void ButtonUpdateJobDomain_Click(object sender, RoutedEventArgs e)
        {
            JobDomain jobDomain = (JobDomain)ListJobDomain.SelectedItem;

            if (jobDomain != null)
            {
                JobDomainWindow jobDomainWindow = new JobDomainWindow(db);
                jobDomainWindow.DataContext = jobDomain;
                jobDomainWindow.ShowDialog();

                if (jobDomainWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un domaine de métier.");
            }
        }

        private void ButtonDeleteJobDomain_Click(object sender, RoutedEventArgs e)
        {
            JobDomain jobDomain = (JobDomain)ListJobDomain.SelectedItem;

            if (jobDomain != null)
            {
                int numberJobs = db.Job.Count(dbJob => dbJob.IdJobDomain == jobDomain.Identifier);

                if (numberJobs == 0)
                {
                    DeleteWindow deleteWindow = new DeleteWindow();
                    deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer le domaine de métier " + jobDomain.Label + " ?";
                    deleteWindow.ShowDialog();

                    // tester si la fenetre deleteWindow renvois un vrai
                    if (deleteWindow.DialogResult == true)
                    {
                        db.JobDomain.Remove(db.JobDomain.First(dbJobDomain => dbJobDomain.Identifier == jobDomain.Identifier));
                        JobDomains.Remove(jobDomain);

                        db.SaveChanges();
                    }
                }
                else
                {
                    MessageBox.Show("Ce domaine de métier contient un ou plusieurs métier. Impossible de le supprimer.");
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un domaine de métier.");
            }
        }
        #endregion

        #region Boutons Métier
        private void ButtonAddJob_Click(object sender, RoutedEventArgs e)
        {
            Job job = new Job();
            job.JobDomain = (JobDomain)ListJobDomain.SelectedItem;
            JobDataContext jobDataContext = new JobDataContext();
            jobDataContext.Job = job;
            jobDataContext.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());
            JobWindow jobWindow = new JobWindow(db);

            jobWindow.DataContext = jobDataContext;
            jobWindow.ShowDialog();

            if (jobWindow.DialogResult == true)
            {
                Jobs.Add(job);
                db.Job.Add(job);

                db.SaveChanges();
            }
        }

        private void ButtonUpdateJob_Click(object sender, RoutedEventArgs e)
        {
            JobDomain jobDomain = (JobDomain)ListJobDomain.SelectedItem;
            Job job = (Job)ListJob.SelectedItem;
            JobDataContext jobDataContext = new JobDataContext();
            jobDataContext.Job = job;
            jobDataContext.JobDomains = new ObservableCollection<JobDomain>(db.JobDomain.ToList());

            if (job != null)
            {
                JobWindow jobWindow = new JobWindow(db);
                jobWindow.DataContext = jobDataContext;
                jobWindow.ShowDialog();

                if (jobWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un métier.");
            }
        }

        private void ButtonDeleteJob_Click(object sender, RoutedEventArgs e)
        {
            Job job = (Job)ListJob.SelectedItem;

            if (job != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();
                deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer le métier " + job.Label + " ?";
                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {
                    db.Job.Remove(db.Job.First(dbJob => dbJob.Identifier == job.Identifier));
                    Jobs.Remove(job);

                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un métier.");
            }
        }
        #endregion

        #region Selection table
        private void ListJobDomain_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            JobDomain jobDomain = (JobDomain)ListJobDomain.SelectedItem;


            if (jobDomain != null)
            {
                ObservableCollection<Job> temp = new ObservableCollection<Job>(db.Job.Where(dbJob => dbJob.IdJobDomain == jobDomain.Identifier).ToList());

                Jobs.Clear();

                foreach (Job job in temp)
                {
                    Jobs.Add(job);
                }
            }
        }
        #endregion

        #region Internautes
        private void ButtonAddWebUser_Click(object sender, RoutedEventArgs e)
        {
            WebUser webUser = new WebUser();
            WebUserWindow webUserWindow = new WebUserWindow(db);

            webUserWindow.DataContext = webUser;
            webUserWindow.ShowDialog();

            if (webUserWindow.DialogResult == true)
            {
                WebUsers.Add(webUser);
                db.WebUser.Add(webUser);

                db.SaveChanges();
            }
        }

        private void ButtonUpdateWebUser_Click(object sender, RoutedEventArgs e)
        {
            WebUser webUser = (WebUser)ListWebUser.SelectedItem;

            if (webUser != null)
            {
                WebUserWindow webUserWindow = new WebUserWindow(db);
                webUserWindow.DataContext = webUser;
                webUserWindow.ShowDialog();

                if (webUserWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez séléctionné aucun client.");
            }
        }

        private void ButtonDeleteWebUser_Click(object sender, RoutedEventArgs e)
        {
            WebUser webUser = (WebUser)ListWebUser.SelectedItem;

            if (webUser != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();
                deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer l'internaute " + webUser.Firstname + " " + webUser.Lastname + " ?";
                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {          
                    // supprimer l'internaute de la base de donnée 
                    db.WebUser.Remove(db.WebUser.First(dbWebUser => dbWebUser.Identifier == webUser.Identifier));

                    // supprimer le l'internaute de la fenétre.
                    WebUsers.Remove(webUser);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un internaute.");
            }
        }
        #endregion

        #region Boutons Partenaire
        private void ButtonAddPartner_Click(object sender, RoutedEventArgs e)
        {
            Partner partner = new Partner();
            PartnerWindow partnerWindow = new PartnerWindow(db);

            partnerWindow.DataContext = partner;
            partnerWindow.ShowDialog();

            if (partnerWindow.DialogResult == true)
            {
                Partners.Add(partner);
                db.Partner.Add(partner);

                db.SaveChanges();
            }
        }

        private void ButtonUpdatePartner_Click(object sender, RoutedEventArgs e)
        {
            Partner partner = (Partner)ListPartner.SelectedItem;

            if (partner != null)
            {
                PartnerWindow partnerWindow = new PartnerWindow(db);
                partnerWindow.DataContext = partner;
                partnerWindow.ShowDialog();

                if (partnerWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un partenaire de diffusion.");
            }
        }

        private void ButtonDeletePartner_Click(object sender, RoutedEventArgs e)
        {
            Partner partner = (Partner)ListPartner.SelectedItem;

            if (partner != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();
                deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer le partenaire " + partner.Account.Email + " ?";
                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {
                    // Si il'y a une offre séléctionée.             
                    // supprimer l'offre de la base de donnée 
                    db.Partner.Remove(db.Partner.First(dbPartner => dbPartner.Identifier == partner.Identifier));

                    // supprimer le l'offre de la fenétre.
                    Partners.Remove(partner);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un partenaire.");
            }
        }
        #endregion

        #region Boutons Type de contrat
        private void ButtonAddTypeOfContract_Click(object sender, RoutedEventArgs e)
        {
            TypeOfContract typeOfContract = new TypeOfContract();
            TypeOfContractWindow typeOfContractWindow = new TypeOfContractWindow(db);

            typeOfContractWindow.DataContext = typeOfContract;
            typeOfContractWindow.ShowDialog();

            if (typeOfContractWindow.DialogResult == true)
            {
                TypeOfContracts.Add(typeOfContract);
                db.TypeOfContract.Add(typeOfContract);

                db.SaveChanges();
            }
        }

        private void ButtonUpdateTypeOfContract_Click(object sender, RoutedEventArgs e)
        {
            TypeOfContract typeOfContract = (TypeOfContract)ListTypeOfContract.SelectedItem;

            if (typeOfContract != null)
            {
                TypeOfContractWindow typeOfContractWindow = new TypeOfContractWindow(db);
                typeOfContractWindow.DataContext = typeOfContract;
                typeOfContractWindow.ShowDialog();

                if (typeOfContractWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un type de contrat.");
            }
        }

        private void ButtonDeleteTypeOfContract_Click(object sender, RoutedEventArgs e)
        {
            TypeOfContract typeOfContract = (TypeOfContract)ListTypeOfContract.SelectedItem;

            if (typeOfContract != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();
                deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer le type de contrat " + typeOfContract.Label + " ?";
                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {
                    // Si il'y a une offre séléctionée.             
                    // supprimer l'offre de la base de donnée 
                    db.TypeOfContract.Remove(db.TypeOfContract.First(dbTypeOfContract => dbTypeOfContract.Identifier == typeOfContract.Identifier));

                    // supprimer le l'offre de la fenétre.
                    TypeOfContracts.Remove(typeOfContract);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un type de contrat.");
            }
        }
        #endregion


        #region Pack
        private void ButtonAddPack_Click(object sender, RoutedEventArgs e)
        {
            Pack pack = new Pack();
            PackWindow packWindow = new PackWindow(db);

            packWindow.DataContext = pack;
            packWindow.ShowDialog();

            if (packWindow.DialogResult == true)
            {
                Packs.Add(pack);
                db.Pack.Add(pack);

                db.SaveChanges();
            }
        }

        private void ButtonUpdatePack_Click(object sender, RoutedEventArgs e)
        {
            Pack pack = (Pack)ListPack.SelectedItem;

            if (pack != null)
            {
                PackWindow packWindow = new PackWindow(db);
                packWindow.DataContext = pack;
                packWindow.ShowDialog();

                if (packWindow.DialogResult == true)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Vous n'avez séléctionné aucun client.");
            }
        }

        private void ButtonDeletePack_Click(object sender, RoutedEventArgs e)
        {
            Pack pack = (Pack)ListPack.SelectedItem;

            if (pack != null)
            {
                DeleteWindow deleteWindow = new DeleteWindow();
                deleteWindow.Description = "Êtes-vous sûr(e) de vouloir supprimer le pack de " + pack.NumberDays + " jour(s) et " + pack.NumberOffers + " offre(s) à " + pack.Price + "€ ? ";
                deleteWindow.ShowDialog();

                // tester si la fenetre deleteWindow renvois un vrai
                if (deleteWindow.DialogResult == true)
                {
                    // supprimer l'internaute de la base de donnée 
                    db.Pack.Remove(db.Pack.First(dbPack => dbPack.Identifier == pack.Identifier));

                    // supprimer le l'internaute de la fenétre.
                    Packs.Remove(pack);

                    //Sauvegarder les changements.
                    db.SaveChanges();
                }
            }
            else
            {
                // Afficher le message d'erreur 
                MessageBox.Show("Veuillez sélectionner un pack.");
            }
        }
        #endregion
    }
}
