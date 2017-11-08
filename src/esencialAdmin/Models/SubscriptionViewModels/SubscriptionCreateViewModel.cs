using esencialAdmin.Data.Models;
using esencialAdmin.Models.GoodiesViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionCreateViewModel
    {
        [DisplayName("Nummer")]
        [Required]
        public int PlantNumber { get; set; }

        [DisplayName("Kunde auswählen")]
        [Required]
        public int CustomerID { get; set; }

        [DisplayName("Patenschaft auswählen")]
        [Required]
        public int PlanID { get; set; }

        [Required]
        [DisplayName("Stichtag")]
        public DateTime StartDate { get; set; }

        [DisplayName("Bezahlt")]
        public bool Payed { get; set; }

        [Display(Name = "Zahlugngssart")]
        public int PaymentMethodID { get; set; }

        [Display(Name = "Zahlugngssart")]
        public virtual List<PaymentMethodsViewModel> PaymentMethods { get; set; }

        [DisplayName("Geschenkt von")]
        public int GiverCustomerId { get; set; }
    }
}
