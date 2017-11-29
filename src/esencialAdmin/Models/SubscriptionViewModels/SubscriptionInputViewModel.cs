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
    public class SubscriptionIndexViewModel
    {

        [Display(Name = "Plan")]
        public virtual List<SubscriptionSelectPlanViewModel> Plans { get; set; }

        public int planID { get; set; }

        [Display(Name = "Status")]
        public virtual List<SubscriptionSelectStatusViewModel> Status { get; set; }

        [DisplayName("Geschenk erhalten?")]
        public bool Goody { get; set; }

        public int statusID { get; set; }

    }
}
