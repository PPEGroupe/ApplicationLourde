﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MegaCasting.DBLib
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MegaCastingEntities : DbContext
    {
        public MegaCastingEntities()
            : base("name=MegaCastingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobDomain> JobDomain { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<Pack> Pack { get; set; }
        public virtual DbSet<PackPaid> PackPaid { get; set; }
        public virtual DbSet<Partner> Partner { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<TypeOfContract> TypeOfContract { get; set; }
        public virtual DbSet<WebUser> WebUser { get; set; }
    }
}
