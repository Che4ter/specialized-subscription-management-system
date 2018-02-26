using System.ComponentModel;

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

        [DisplayName("Anzahl ausgelaufene Patenschaften")]
        public int NumberOfEndedSubscriptions { get; set; }
    }
}