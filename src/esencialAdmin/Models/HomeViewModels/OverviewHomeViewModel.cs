using esencialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.HomeViewModels
{
    public class OverviewHomeViewModel
    {

        [DisplayName("Anzahl Kunden")]
        public int NumberOfCustomers { get; set; }

        [DisplayName("Anzahl Patenschaften")]
        public int NumberOfSubscriptions { get; set; }

        [DisplayName("Anzahl aktive Patenschaften")]
        public int NumberOfActiveSubscriptions { get; set; }

        [DisplayName("Anzahl nicht bezahlte Patenschaften")]
        public int NumberOfNotPayedSubscriptions { get; set; }

        [DisplayName("Anzahl auslaufende Patenschaften")]
        public int NumberOfEndingSubscriptions { get; set; }


    }
}
