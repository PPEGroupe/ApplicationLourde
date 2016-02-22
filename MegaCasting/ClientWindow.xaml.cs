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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private MegaCastingEntities db = new MegaCastingEntities();

        public ClientWindow()
        {
            InitializeComponent();

            Client client = new Client();

            client.Identifier = 00222;
            client.ZipCode = "53000";
            client.PhoneNumber = "03165986532";
            client.URL = "www.client.com";
            client.Address = "39 rue franche comté 53000";
            client.City = "Laval";
            client.Email = "client@client.com";
            client.Fax = "0649";

            this.DataContext = client;

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void ButtonValidate_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}
