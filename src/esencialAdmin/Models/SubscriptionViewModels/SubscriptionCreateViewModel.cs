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
        public int PlantNumber { get; set; }

        [DisplayName("Kunde")]
        public SubscriptionCustomerViewModel Customer { get; set; }

        [DisplayName("Schenker")]
        public SubscriptionCustomerViewModel GiverCustomer { get; set; }

        [DisplayName("Plan")]
        public int PlanID { get; set; }

        [DisplayName("Pläne")]
        public List<SubscriptionPlanViewModel> Plans { get; set; }

        [DisplayName("Start Datum")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [DisplayName("Bezahlt")]
        public bool Payed { get; set; }
    }
}
