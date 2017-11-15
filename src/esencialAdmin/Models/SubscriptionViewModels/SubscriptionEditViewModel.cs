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
    public class SubscriptionEditViewModel
    {
        [DisplayName("Id")]
        public int ID { get; set; }

        [DisplayName("Nummer")]
        [Required]
        public int PlantNumber { get; set; }

        [DisplayName("Kunde")]
        public SubscriptionCustomerViewModel Customer { get; set; }

        [DisplayName("Patenschaft")]
        public SubscriptionPlanViewModel Plan { get; set; }

        [DisplayName("Perioden")]
        public List<SubscriptionPeriodeViewModel>  Periodes { get; set; }

        public DateTime? DateCreated { get; set; }
        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }
    }
}
