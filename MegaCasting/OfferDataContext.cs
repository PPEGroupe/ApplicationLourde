using MegaCasting.DBLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCasting
{
    class OfferDataContext
    {
        public Offer Offer { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
        public ObservableCollection<TypeOfContract> TypeOfContracts { get; set; }
    }
}
