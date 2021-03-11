using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Covoiturage.Models
{
    public class RechercherTrajetViewModel
    {
        public string Lieu_Depart { get; set; }
        public string Lieu_Arrivee { get; set; }
        public DateTime Date_Depart { get; set; }
    }
}