using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public String CustomerPreSelect { get; set; }

        [DisplayName("Patenschaft auswählen")]
        [Required]
        public int PlanID { get; set; }

        public String PlanPreSelect { get; set; }

        [Required]
        [DisplayName("Start Datum")]
        public DateTime StartDate { get; set; }

        [DisplayName("Bezahlt")]
        public bool Payed { get; set; }

        [Display(Name = "Zahlungsmethode")]
        public int PaymentMethodID { get; set; }

        [Display(Name = "Zahlugngssart")]
        public virtual List<PaymentMethodsViewModel> PaymentMethods { get; set; }

        [DisplayName("Geschenkt von")]
        public int GiverCustomerId { get; set; }

        public String GiverPreSelect { get; set; }

        [DisplayName("Bemerkungen")]
        public string SubscriptionRemarks { get; set; }
    }
}