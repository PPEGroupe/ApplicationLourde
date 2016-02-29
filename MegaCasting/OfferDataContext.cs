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
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        public ObservableCollection<Job> Jobs { get; set; }
    }
}
