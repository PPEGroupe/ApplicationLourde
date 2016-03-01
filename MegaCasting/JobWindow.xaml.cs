﻿using System;
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
using System.Collections.ObjectModel;
using MegaCasting.DBLib;

namespace MegaCasting
{
    /// <summary>
    /// Logique d'interaction pour JobWindow.xaml
    /// </summary>
    public partial class JobWindow : Window
    {
        #region Attributs
        private MegaCastingEntities db;
        public ObservableCollection<Job> Jobs { get; set; }
        public ObservableCollection<JobDomain> JobDomains { get; set; }
        #endregion

        #region Constructeur
        public JobWindow(MegaCastingEntities context)
        {
            db = context;

            InitializeComponent();
        }
        #endregion
    }
}
