//------------------------------------------------------------------------------
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
    using System.Collections.ObjectModel;
    
    public partial class Offer
    {
        public long Identifier { get; set; }
        public string Label { get; set; }
        public string Reference { get; set; }
        public System.DateTime DateStartPublication { get; set; }
        public int PublicationDuration { get; set; }
        public System.DateTime DateStartContract { get; set; }
        public int JobQuantity { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string JobDescription { get; set; }
        public string ProfileDescription { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public long IdTypeOfContrat { get; set; }
        public long IdJob { get; set; }
        public long IdClient { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Job Job { get; set; }
        public virtual TypeOfContract TypeOfContract { get; set; }
    }
}