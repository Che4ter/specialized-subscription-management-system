using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionEditViewModel
    {
        [DisplayName("Id")]
        public int ID { get; set; }

        [DisplayName("Reben Nummer")]
        public int PlantNumber { get; set; }

        [DisplayName("Kunde")]
        public SubscriptionCustomerViewModel Customer { get; set; }

        [DisplayName("Patenschaft")]
        public SubscriptionPlanViewModel Plan { get; set; }

        [DisplayName("Perioden")]
        public List<SubscriptionPeriodeViewModel>  Periodes { get; set; }

        public Dictionary<int,String> Photos { get; set; }

        public int StatusID { get; set; }
        public int TemplateID { get; set; }

        [DisplayName("Patenschaft Status")]
        public String StatusLabel { get; set; }

        [DisplayName("Erstellt am")]
        public string DateCreated { get; set; }

        [DisplayName("Erstellt durch")]
        public string UserCreated { get; set; }

        [DisplayName("Zuletzt bearbeitet am")]
        public string DateModified { get; set; }

        [DisplayName("Zuletzt bearbeitet durch")]
        public string UserModified { get; set; }
    }
}