//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class appointment
    {
        public int idAppointment { get; set; }
        public System.DateTime dateHour { get; set; }
        public int idCustomer { get; set; }
        public int idBroker { get; set; }
    
        public virtual broker broker { get; set; }
        public virtual customer customer { get; set; }
    }
}
